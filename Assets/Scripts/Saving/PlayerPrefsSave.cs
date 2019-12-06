using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSave : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerHandler player;
    public bool custom;
    private void Awake()
    {

    }

    private void Start()
    {
        if (!custom)
        {
            BinaryLoad();
        }
    }

    public void PrefsSave()
    {
        // Health, Mana, Stamina
        PlayerPrefs.SetFloat("currentHealth", player.currentHealth);
        PlayerPrefs.SetFloat("currentMana", player.currentMana);
        PlayerPrefs.SetFloat("currentStamina", player.currentStamina);

        // Rotation
        PlayerPrefs.SetFloat("currentRotationW", player.transform.rotation.w);
        PlayerPrefs.SetFloat("currentRotationX", player.transform.rotation.x);
        PlayerPrefs.SetFloat("currentRotationY", player.transform.rotation.y);
        PlayerPrefs.SetFloat("currentRotationZ", player.transform.rotation.z);

        // Position
        PlayerPrefs.SetFloat("currentPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("currentPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("currentPositionZ", player.transform.position.z);

        // Checkpoint
        PlayerPrefs.SetFloat("currentCheckpointX", player.currentCheckPoint.position.x);
        PlayerPrefs.SetFloat("currentCheckpointY", player.currentCheckPoint.position.y);
        PlayerPrefs.SetFloat("currentCheckpointZ", player.currentCheckPoint.position.z);
    }

    public void PrefsLoad()
    {
        // Health, Mana, Stamina
        player.currentHealth = PlayerPrefs.GetFloat("currentHealth", player.maxHealth);
        player.currentMana = PlayerPrefs.GetFloat("currentMana", player.maxMana);
        player.currentStamina = PlayerPrefs.GetFloat("currentStamina", player.maxStamina);

        // Rotation
        player.transform.rotation = new Quaternion(PlayerPrefs.GetFloat("currentRotationX", 0f), PlayerPrefs.GetFloat("currentRotationY", 0f), PlayerPrefs.GetFloat("currentRotationZ", 0f), PlayerPrefs.GetFloat("currentRotationW", 0f));

        // Position
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("currentPositionX", 5f), PlayerPrefs.GetFloat("currentPositionY", 1.5f), PlayerPrefs.GetFloat("currentPositionZ", 5f));

        // Checkpoint
        player.currentCheckPoint.position = new Vector3(PlayerPrefs.GetFloat("currentCheckpointX", 5f), PlayerPrefs.GetFloat("currentCheckpointY", 5f), PlayerPrefs.GetFloat("currentCheckpointZ", 5f));

    }

    public void BinarySave()
    {
        PlayerSaveToBinary.SavePlayerData(player);
    }

    public void BinaryLoad()
    {
        PlayerDataToSave data = PlayerSaveToBinary.LoadData(player);

        player.maxHealth = data.maxHealth;
        player.maxMana = data.maxMana;
        player.maxStamina = data.maxStamina;

        player.currentHealth = data.currentHealth;
        player.currentMana = data.currentMana;
        player.currentStamina = data.currentStamina;

        player.currentCheckPoint = GameObject.Find(data.checkpoint).GetComponent<Transform>();

        if (!(data.currentPositionX == 0 && data.currentPositionY == 0 && data.currentPositionZ == -5))
        {
            player.transform.position = new Vector3(data.currentPositionX, data.currentPositionY, data.currentPositionZ);
            player.transform.rotation = new Quaternion(data.currentRotationX, data.currentRotationY, data.currentRotationZ, data.currentRotationW);
        }
        else
        {
            player.transform.position = player.currentCheckPoint.position;
            player.transform.rotation = player.currentCheckPoint.rotation;
            Debug.Log("Loading character at " + player.currentCheckPoint.position);
        }

        player.skinIndex = data.skinIndex;
        player.hairIndex = data.hairIndex;
        player.mouthIndex = data.mouthIndex;
        player.eyesIndex = data.eyesIndex;
        player.clothesIndex = data.clothesIndex;
        player.armourIndex = data.armourIndex;

        player.characterClass = data.characterClass;
        player.characterName = data.characterName;

        for (int i = 0; i < player.stats.Length; i++)
        {
            player.stats[i].value = data.stats[i];
        }

        LoadCustomisation.SetTexture("Skin", data.skinIndex);
        LoadCustomisation.SetTexture("Hair", data.hairIndex);
        LoadCustomisation.SetTexture("Mouth", data.mouthIndex);
        LoadCustomisation.SetTexture("Eyes", data.eyesIndex);
        LoadCustomisation.SetTexture("Clothes", data.clothesIndex);
        LoadCustomisation.SetTexture("Armour", data.armourIndex);
    }
}
