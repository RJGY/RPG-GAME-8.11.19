using UnityEngine;
[System.Serializable]

public class PlayerDataToSave
{
    // Data from game

    public string playerName;
    public int level;
    public string checkpoint;
    public float maxHealth, maxMana, maxStamina;
    public float currentHealth, currentMana, currentStamina;
    public float currentPositionX, currentPositionY, currentPositionZ;
    public float currentRotationX, currentRotationY, currentRotationZ, currentRotationW;
    public int skinIndex, hairIndex, mouthIndex, eyesIndex, clothesIndex, armourIndex;
    public Customization.CharacterClass characterClass;
    public string characterName;
    public int[] stats = new int[6];
    public static int saveSlot;

    public PlayerDataToSave(PlayerHandler player)
    {
        playerName = player.name;
        level = 0;
        if (player.currentCheckPoint != null)
        {
            checkpoint = player.currentCheckPoint.name;
        }
        else
        {
            checkpoint = player.firstCheckPointName;
        }
        

        maxHealth = player.maxHealth;
        maxMana = player.maxMana;
        maxStamina = player.maxStamina;

        currentHealth = player.currentHealth;
        currentMana = player.currentMana;
        currentStamina = player.currentStamina;

        currentPositionX = player.transform.position.x;
        currentPositionY = player.transform.position.y;
        currentPositionZ = player.transform.position.z;

        currentRotationX = player.transform.rotation.x;
        currentRotationW = player.transform.rotation.w;
        currentRotationY = player.transform.rotation.y;
        currentRotationZ = player.transform.rotation.z;

        skinIndex = player.skinIndex;
        hairIndex = player.hairIndex;
        mouthIndex = player.mouthIndex;
        eyesIndex = player.eyesIndex;
        clothesIndex = player.clothesIndex;
        armourIndex = player.armourIndex;

        characterClass = player.characterClass;
        characterName = player.characterName;
        for (int i = 0; i < player.stats.Length; i++)
        {
            stats[i] = player.stats[i].value;
        }
    }
}
