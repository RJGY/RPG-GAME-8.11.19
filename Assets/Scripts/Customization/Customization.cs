using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Customization : MonoBehaviour
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
    public CharacterClass characterClass;
    public Vector2 screen = new Vector2(Screen.width / 16, Screen.height / 9);

    public enum CharacterClass
    {
        Barbarian,
        Bard,
        Cleric,
        Druid,
        Fighter,
        Monk,
        Paladin,
        Ranger,
        Rogue,
        Sorcerer,
        Warlock,
        Wizard
    }

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

    public void OnGUI()
    {
        DisplayCustom();
        DisplayStats();
        if (GUI.Button(new Rect(screen.x * 13.25f, screen.y * 5, screen.x * 2f, screen.y * 0.5f), "Save"))
        {
            Save();
        }
    }

    void DisplayCustom()
    {
        /*
        int i = 0;
        #region Skins
        if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
        {
            SetTexture("Skin", -1);
        }

        GUI.Box(new Rect(screen.x * 0.75f, screen.y + i * (0.5f * screen.y), screen.x, screen.y * 0.5f), "Skin");

        if (GUI.Button(new Rect(screen.x * 1.75f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
        {
            SetTexture("Skin", 1);
        }
        i++;
        #endregion

        #region Eyes
        if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
        {
            SetTexture("Eyes", -1);
        }

        GUI.Box(new Rect(screen.x * 0.75f, screen.y + i * (0.5f * screen.y), screen.x, screen.y * 0.5f), "Eyes");

        if (GUI.Button(new Rect(screen.x * 1.75f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
        {
            SetTexture("Eyes", 1);
        }
        i++;
        #endregion

        #region Hair
        if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
        {
            SetTexture("Hair", -1);
        }

        GUI.Box(new Rect(screen.x * 0.75f, screen.y + i * (0.5f * screen.y), screen.x, screen.y * 0.5f), "Hair");

        if (GUI.Button(new Rect(screen.x * 1.75f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
        {
            SetTexture("Hair", 1);
        }
        i++;
        #endregion

        #region Mouth
        if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
        {
            SetTexture("Mouth", -1);
        }

        GUI.Box(new Rect(screen.x * 0.75f, screen.y + i * (0.5f * screen.y), screen.x, screen.y * 0.5f), "Mouth");

        if (GUI.Button(new Rect(screen.x * 1.75f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
        {
            SetTexture("Mouth", 1);
        }
        i++;
        #endregion

        #region Clothes
        if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
        {
            SetTexture("Clothes", -1);
        }

        GUI.Box(new Rect(screen.x * 0.75f, screen.y + i * (0.5f * screen.y), screen.x, screen.y * 0.5f), "Clothes");

        if (GUI.Button(new Rect(screen.x * 1.75f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
        {
            SetTexture("Clothes", 1);
        }
        i++;
        #endregion

        #region Armour
        if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
        {
            SetTexture("Armour", -1);
        }

        GUI.Box(new Rect(screen.x * 0.75f, screen.y + i * (0.5f * screen.y), screen.x, screen.y * 0.5f), "Armour");

        if (GUI.Button(new Rect(screen.x * 1.75f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
        {
            SetTexture("Armour", 1);
        }
        i++;
        #endregion
        */

        string[] types = { "Skin", "Eyes", "Mouth", "Hair", "Clothes", "Armour" };
        for (int x = 0; x < types.Length; x++)
        {
            if (GUI.Button(new Rect(screen.x * 13.25f, screen.y + x * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
            {
                SetTexture(types[x], -1);
            }

            GUI.Box(new Rect(screen.x * 13.75f, screen.y + x * (0.5f * screen.y), screen.x, screen.y * 0.5f), types[x]);

            if (GUI.Button(new Rect(screen.x * 14.75f, screen.y + x * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
            {
                SetTexture(types[x], 1);
            }
        }

        if (GUI.Button(new Rect(screen.x * 13.25f, screen.y * 4, screen.x * 2f, screen.y * 0.5f), "Reset"))
        {
            skinIndex = 0;
            eyesIndex = 0;
            mouthIndex = 0;
            hairIndex = 0;
            clothesIndex = 0;
            armourIndex = 0;

            for (int i = 0; i < types.Length; i++)
            {
                SetTexture(types[i], 0);
            }
        }

        if (GUI.Button(new Rect(screen.x * 13.25f, screen.y * 4.5f, screen.x * 2f, screen.y * 0.5f), "Random"))
        {
            skinIndex = Random.Range(0, skin.Capacity);
            eyesIndex = Random.Range(0, eyes.Capacity);
            mouthIndex = Random.Range(0, mouth.Capacity);
            hairIndex = Random.Range(0, hair.Capacity);
            clothesIndex = Random.Range(0, clothes.Capacity);
            armourIndex = Random.Range(0, armour.Capacity);

            for (int i = 0; i < types.Length; i++)
            {
                SetTexture(types[i], 0);
            }
        }
    }

    void DisplayStats()
    {
        characterName = GUI.TextField(new Rect(screen.x * 6, screen.y * 7.5f, screen.x * 4, screen.y * 0.5f), characterName, 20, gUINamePlateStyle);

        #region Class
        int i = 0;

        if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "<-"))
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

        GUI.Box(new Rect(screen.x * 0.75f, screen.y + i * (0.5f * screen.y), screen.x * 1.5f, screen.y * 0.5f), characterClass.ToString());

        if (GUI.Button(new Rect(screen.x * 2.25f, screen.y + i * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "->"))
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
        i++;
        #endregion

        #region Stat Distribution
        

        GUI.Box(new Rect(screen.x * 0.25f, screen.y + i * (0.5f * screen.y), screen.x * 2.5f, screen.y * 0.5f), "Points " + extraPoints);

        for (int x = 0; x < playerStats.Length; x++)
        {
            if (playerStats[x].tempValue > 0)
            {
                if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + (2 + x) * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "-"))
                {
                    extraPoints++;
                    playerStats[x].tempValue--;
                }
            }

            GUI.Box(new Rect(screen.x * 0.75f, screen.y + (2 + x) * (0.5f * screen.y), screen.x * 1.5f, screen.y * 0.5f), playerStats[x].statName + ": " + (playerStats[x].statValue + playerStats[x].tempValue));

            if (extraPoints > 0)
            {
                if (GUI.Button(new Rect(screen.x * 2.25f, screen.y + (2 + x) * (0.5f * screen.y), screen.x * 0.5f, screen.y * 0.5f), "+"))
                {
                    extraPoints--;
                    playerStats[x].tempValue++;
                }
            }

            if (x == playerStats.Length - 1)
            {
                if (GUI.Button(new Rect(screen.x * 0.25f, screen.y + (3 + x) * (0.5f * screen.y), screen.x * 2.5f, screen.y * 0.5f), "Reset"))
                {
                    extraPoints = 10;
                    for (int y = 0; y < playerStats.Length; y++)
                    {
                        playerStats[y].tempValue = 0;
                    }
                }
            }
        }

        #endregion


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
                characterClass = CharacterClass.Barbarian;

                break;

            case 1:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 10; // CON
                playerStats[3].statValue = 15; // WIS
                playerStats[4].statValue = 5; // INT
                playerStats[5].statValue = 20; // CHA
                characterClass = CharacterClass.Bard;
                break;

            case 2:
                playerStats[0].statValue = 6; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 6; // DEX
                playerStats[2].statValue = 8; // CON
                playerStats[3].statValue = 15; // WIS
                playerStats[4].statValue = 15; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = CharacterClass.Cleric;
                break;

            case 3:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 10; // CON
                playerStats[3].statValue = 15; // WIS
                playerStats[4].statValue = 15; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = CharacterClass.Druid;
                break;

            case 4:
                playerStats[0].statValue = 15; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 10; // DEX
                playerStats[2].statValue = 15; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 5; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = CharacterClass.Fighter;
                break;

            case 5:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 10; // DEX
                playerStats[2].statValue = 10; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 10; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = CharacterClass.Monk;
                break;

            case 6:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 15; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 10; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = CharacterClass.Paladin;
                break;

            case 7:
                playerStats[0].statValue = 10; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 15; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 10; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = CharacterClass.Ranger;
                break;

            case 8:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 25; // INT
                playerStats[5].statValue = 10; // CHA
                characterClass = CharacterClass.Sorcerer;
                break;

            case 9:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 20; // WIS
                playerStats[4].statValue = 20; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = CharacterClass.Warlock;
                break;

            case 10:
                playerStats[0].statValue = 5; // STR strength, dexterity, constitution, wisdom, intelligence, charisma;
                playerStats[1].statValue = 5; // DEX
                playerStats[2].statValue = 5; // CON
                playerStats[3].statValue = 10; // WIS
                playerStats[4].statValue = 30; // INT
                playerStats[5].statValue = 5; // CHA
                characterClass = CharacterClass.Wizard;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
