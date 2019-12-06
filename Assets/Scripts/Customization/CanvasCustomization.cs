using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasCustomization : MonoBehaviour
{
    // save to binary all the stat values (and both together) and name and indexes.

    public Renderer characterRend;
    public Renderer backpack;
    public Renderer axe;
    public GUIStyle gUINamePlateStyle;

    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();

    public int skinIndex, eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex, selectedIndex, extraPoints = 10;
    public int skinMax, eyesMax, mouthMax, hairMax, clothesMax, armourMax;
    public string characterName = "Adventurer";

    public PlayerPrefsSave saveNew;
    public PlayerHandler player;

    /*
    public int strength, dexterity, constitution, wisdom, intelligence, charisma;
    public int strengthTemp, dexterityTemp, constitutionTemp, wisdomTemp, intelligenceTemp, charismaTemp;
    */
    [System.Serializable]
    public struct Stats
    {
        public string statName;
        public int statValue;
        public int tempValue;
    };

    public Stats[] playerStats = new Stats[6];
    public Customization.CharacterClass characterClass;
    public Vector2 screen = new Vector2(Screen.width / 16, Screen.height / 9);

    // Canvas
    [Header("Canvas Elements")]
    private Dictionary<string, GameObject> displayButtons = new Dictionary<string, GameObject>();
    private Dictionary<string, Button> addStatButtons = new Dictionary<string, Button>();
    private Dictionary<string, Button> backStatButtons = new Dictionary<string, Button>();
    [SerializeField]
    public GameObject classDisplayButton;
    public GameObject forwardStatButtonHolder;
    public GameObject backStatButtonHolder;
    public GameObject displayVerticalHolder;
    public Button forwardStatButton;
    public GameObject displayStatButton;
    public Button backStatButton;
    public Text pointsText;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerHandler>();
        saveNew = GetComponent<PlayerPrefsSave>();
        Time.timeScale = 1;
        player.custom = true;
        saveNew.custom = true;
        saveNew.player = player;
        player.saveAndLoad = saveNew;

        for (int i = 0; i < skinMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            skin.Add(tempTexture);
        }

        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;
            eyes.Add(tempTexture);
        }

        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Mouth_" + i.ToString()) as Texture2D;
            mouth.Add(tempTexture);
        }

        for (int i = 0; i < hairMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;
            hair.Add(tempTexture);
        }

        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Clothes_" + i.ToString()) as Texture2D;
            clothes.Add(tempTexture);
        }

        for (int i = 0; i < armourMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Armour_" + i.ToString()) as Texture2D;
            armour.Add(tempTexture);
        }

        SetTexture("Skin", 0);
        SetTexture("Eyes", 0);
        SetTexture("Mouth", 0);
        SetTexture("Hair", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);

        ChooseClass(selectedIndex);

        // Displays stats.
        foreach (Stats stat in playerStats)
        {
            // Creates buttons
            GameObject displayButton = Instantiate(displayStatButton, displayVerticalHolder.transform);
            // Adds them to a dictionary
            displayButtons.Add(stat.statName, displayButton);
        }

        // Adds Buttons for each stat
        foreach (Stats stat in playerStats)
        {
            // Creates buttons
            Button addButton = Instantiate(forwardStatButton, forwardStatButtonHolder.transform);
            Button minusButton = Instantiate(backStatButton, backStatButtonHolder.transform);

            // Adds them to a dictionary
            addStatButtons.Add(stat.statName, addButton);
            backStatButtons.Add(stat.statName, minusButton);
        }

        foreach (Stats stat in playerStats)
        {
            foreach (KeyValuePair<string, Button> button in addStatButtons)
            {
                if (button.Key.Equals(stat.statName))
                {
                    button.Value.onClick.AddListener(() => StatDistribution(stat.statName, 1));
                    break;
                }
            }
            foreach (KeyValuePair<string, Button> button in backStatButtons)
            {
                if (button.Key.Equals(stat.statName))
                {
                    button.Value.onClick.AddListener(() => StatDistribution(stat.statName, -1));
                    break;
                }
            }
        }

        UpdateStats();
        UpdateClass();
    }

    private void UpdateStats()
    {
        // make the text element in the gameobject to the stat name and display the stats.
        foreach (KeyValuePair<string, GameObject> keyValuePair in displayButtons)
        {
            foreach (Stats stat in playerStats)
            {
                // If they equal each other, chagne the text to "Strength: 9001"
                if (stat.statName.Equals(keyValuePair.Key))
                {
                    keyValuePair.Value.GetComponentInChildren<Text>().text = stat.statName + ": " + (stat.statValue + stat.tempValue);
                }
            }
        }
        // Set extra poitns text to number of extra points.
        pointsText.text = "Points: " + extraPoints;
    }

    void UpdateClass()
    {
        classDisplayButton.GetComponentInChildren<Text>().text = characterClass.ToString();
    }

    public void ChangeClass(int direction)
    {
        if (direction == -1)
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = 10;
            }

            extraPoints = 10;
            for (int y = 0; y < playerStats.Length; y++)
            {
                playerStats[y].tempValue = 0;
            }

            ChooseClass(selectedIndex);
        }
        else
        {
            selectedIndex++;
            if (selectedIndex > 10)
            {
                selectedIndex = 0;
            }

            extraPoints = 10;
            for (int y = 0; y < playerStats.Length; y++)
            {
                playerStats[y].tempValue = 0;
            }

            ChooseClass(selectedIndex);
        }
        UpdateStats();
        UpdateClass();
    }
    public void Save()
    {
        player.maxHealth = 100 + ((playerStats[0].statValue + playerStats[0].tempValue) * 5);
        player.maxMana = 100 + ((playerStats[3].statValue + playerStats[3].tempValue) * 5);
        player.maxStamina = 100 + ((playerStats[1].statValue + playerStats[1].tempValue) * 5);

        player.currentHealth = player.maxHealth;
        player.currentMana = player.maxMana;
        player.currentStamina = player.maxStamina;

        player.skinIndex = skinIndex;
        player.hairIndex = hairIndex;
        player.mouthIndex = mouthIndex;
        player.eyesIndex = eyesIndex;
        player.clothesIndex = clothesIndex;
        player.armourIndex = armourIndex;

        player.characterClass = characterClass;
        player.characterName = characterName;
        for (int i = 0; i < playerStats.Length; i++)
        {
            player.stats[i].value = playerStats[i].statValue + playerStats[i].tempValue;
        }
        saveNew.BinarySave();
        SceneManager.LoadScene(2);
    }

    public void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                matIndex = 1;
                textures = skin.ToArray();
                break;

            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                matIndex = 2;
                textures = eyes.ToArray();
                break;

            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                matIndex = 3;
                textures = mouth.ToArray();
                break;

            case "Hair":
                index = hairIndex;
                max = hairMax;
                matIndex = 4;
                textures = hair.ToArray();
                break;

            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                matIndex = 5;
                textures = clothes.ToArray();
                break;

            case "Armour":
                index = armourIndex;
                max = armourMax;
                matIndex = 6;
                textures = armour.ToArray();
                break;

        }

        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }

        else if (index > max - 1)
        {
            index = 0;
        }

        Material[] mat = characterRend.materials;
        mat[matIndex].mainTexture = textures[index];
        characterRend.materials = mat;

        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;

            case "Eyes":
                eyesIndex = index;
                break;

            case "Mouth":
                mouthIndex = index;
                break;

            case "Hair":
                hairIndex = index;
                break;

            case "Clothes":
                clothesIndex = index;
                break;

            case "Armour":
                armourIndex = index;
                break;
        }
    }

    public void Reset()
    {
        // Reset Button
        string[] types = { "Skin", "Eyes", "Mouth", "Hair", "Clothes", "Armour" };

        skinIndex = 0;
        eyesIndex = 0;
        mouthIndex = 0;
        hairIndex = 0;
        clothesIndex = 0;
        armourIndex = 0;

        foreach (string type in types)
        {
            SetTexture(type, 0);
        }
    }

    public void RandomSkin()
    {
        string[] types = { "Skin", "Eyes", "Mouth", "Hair", "Clothes", "Armour" };

        skinIndex = Random.Range(0, skin.Count);
        eyesIndex = Random.Range(0, eyes.Count);
        mouthIndex = Random.Range(0, mouth.Count);
        hairIndex = Random.Range(0, hair.Count);
        clothesIndex = Random.Range(0, clothes.Count);
        armourIndex = Random.Range(0, armour.Count);

        foreach (string type in types)
        {
            SetTexture(type, 0);
        }
    }

    public void ChangeCustomForward(string type)
    {
        string[] types = { "Skin", "Eyes", "Mouth", "Hair", "Clothes", "Armour" };

        foreach (string customType in types)
        {
            if (type.Equals(customType))
            {
                SetTexture(customType, 1);
            }
        }
    }
    public void ChangeCustomBackward(string type)
    {
        string[] types = { "Skin", "Eyes", "Mouth", "Hair", "Clothes", "Armour" };

        foreach (string customType in types)
        {
            if (type.Equals(customType))
            {
                SetTexture(customType, -1);
            }
        }
    }

    void StatDistribution(string statName, int direction)
    {
        // If you try to add more points but have no points, stop the user.
        if (direction == 1 && extraPoints <= 0)
        {
            return;
        }

        // Loop through all stats
        for (int i = 0; i < playerStats.Length; i++)
        {
            Debug.Log("I GOT HERE");
            if (playerStats[i].statName.Equals(statName))
            {
                // If they try to minus stats from 0 we stop the user
                if (direction == -1 && playerStats[i].tempValue <= 0)
                {
                    return;
                }
                // Add or remove points from temp values and extra points.
                playerStats[i].tempValue += direction;
                extraPoints -= direction;
            }
        }
        
        UpdateStats();
    }

    public void ResetStats()
    {
        // Reset temp values of stats to 0
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i].tempValue = 0;
        }
        // set extra points to 10
        extraPoints = 10;
        // Update stat values
        UpdateStats();
    }

    void ChooseClass(int className)
    {
        switch (className)
        {
            case 0:
                playerStats[0].statValue = 35;
                playerStats[1].statValue = 5;
                playerStats[2].statValue = 5;
                playerStats[3].statValue = 5;
                playerStats[4].statValue = 5;
                playerStats[5].statValue = 5;
                characterClass = Customization.CharacterClass.Barbarian;

                break;

            case 1:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 10; // CON
                playerStats[3].statValue = 15; // WIS
                playerStats[4].statValue = 5; // INT
                playerStats[5].statValue = 20; // CHA
                characterClass = Customization.CharacterClass.Bard;
                break;

            case 2:
                playerStats[0].statValue = 6; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 6; // DEX
                playerStats[2].statValue = 8; // CON
                playerStats[3].statValue = 15; // WIS
                playerStats[4].statValue = 15; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = Customization.CharacterClass.Cleric;
                break;

            case 3:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 10; // CON
                playerStats[3].statValue = 15; // WIS
                playerStats[4].statValue = 15; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = Customization.CharacterClass.Druid;
                break;

            case 4:
                playerStats[0].statValue = 15; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 10; // DEX
                playerStats[2].statValue = 15; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 5; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = Customization.CharacterClass.Fighter;
                break;

            case 5:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 10; // DEX
                playerStats[2].statValue = 10; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 10; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = Customization.CharacterClass.Monk;
                break;

            case 6:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 15; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 10; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = Customization.CharacterClass.Paladin;
                break;

            case 7:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 15; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 10; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = Customization.CharacterClass.Ranger;
                break;

            case 8:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 25; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = Customization.CharacterClass.Sorcerer;
                break;

            case 9:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 20; // WIS
                playerStats[4].statValue = 20; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = Customization.CharacterClass.Warlock;
                break;

            case 10:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 30; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = Customization.CharacterClass.Wizard;
                break;
        }
    }
}


