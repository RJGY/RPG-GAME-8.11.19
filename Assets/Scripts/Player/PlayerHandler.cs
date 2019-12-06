using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHandler : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerStats
    {
        public string name;
        public int value;
    };

    [Header("Value Variables")]
    public float currentHealth;
    public float currentMana, currentStamina, maxHealth, maxMana, maxStamina, healRate;
    public PlayerStats[] stats;
    [Header("Value Variables")]
    public Slider healthBar;
    public Slider manaBar, staminaBar;
    [Header("Damage Effect Variables")]
    public Image damageImage;
    public Image deathImage;
    public Text text;
    public AudioClip deathClip;
    public float flashSpeed = 5;
    public Color flashColour = new Color(1, 0, 0, 0.2f);
    AudioSource playerAudio;
    public static bool isDead;
    bool damaged;
    bool canHeal;
    float healTimer;
    [Header("Check Point")]
    public Transform currentCheckPoint;

    [Header("Save")]
    public PlayerPrefsSave saveAndLoad;
    [Header("Custom")]
    public bool custom;
    public int skinIndex, eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex;
    public Customization.CharacterClass characterClass;
    public string characterName;
    public string firstCheckPointName = "First CheckPoint";
    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(!custom)
        {
            //Display Health
            if (healthBar.value != Mathf.Clamp01(currentHealth / maxHealth))
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                healthBar.value = Mathf.Clamp01(currentHealth / maxHealth);
            }
            if (manaBar.value != Mathf.Clamp01(currentMana / maxMana))
            {
                currentMana = Mathf.Clamp(currentMana, 0, maxMana);
                manaBar.value = Mathf.Clamp01(currentMana / maxMana);
            }
            if (staminaBar.value != Mathf.Clamp01(currentStamina / maxStamina))
            {
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
                staminaBar.value = Mathf.Clamp01(currentStamina / maxStamina);
            }
            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
#if UNITY_EDITOR
            //Damage
            if (Input.GetKeyDown(KeyCode.X))
            {
                damaged = true;
                currentHealth -= 5;
            }
#endif

            if (damaged && !isDead)
            {
                damageImage.color = flashColour;
                damaged = false;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            if (!canHeal && currentHealth < maxHealth && currentHealth > 0)
            {
                healTimer += Time.deltaTime;
                if (healTimer >= 5)
                {
                    canHeal = true;
                }
            }
        }       
    }
    private void LateUpdate()
    {
        if (currentHealth < maxHealth && currentHealth > 0 && canHeal)
        {
            HealOverTime();
        }
    }
    void Death()
    {
        //Set the death flag to this function isnt called again
        isDead = true;
        text.text = "";

        //Set the AudioSource to play the death clip
        playerAudio.clip = deathClip;
        playerAudio.Play();
        deathImage.gameObject.GetComponent<Animator>().SetTrigger("isDead");
        Invoke("DeathText", 2f);
        Invoke("ReviveText", 6f);
        Invoke("Revive", 9f);
    }
    void Revive()
    {
        text.text = "";
        isDead = false;
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;

        //more and rotate to spawn location
        transform.position = currentCheckPoint.position;
        transform.rotation = currentCheckPoint.rotation;

        deathImage.gameObject.GetComponent<Animator>().SetTrigger("Revive");
    }
    void DeathText()
    {
        text.text = "You've Fallen in Battle...";
    }
    void ReviveText()
    {
        text.text = "...But the Gods have decided it isn't your time...";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckPoint = other.transform;
            healRate = 5;
            saveAndLoad.BinarySave();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            healRate = 0;
        }
    }
    public void DamagePlayer(float damage)
    {
        damaged = true;
        currentHealth -= damage;
        canHeal = false;
        healTimer = 0;
    }
    public void HealOverTime()
    {
        currentHealth += Time.deltaTime * (healRate + stats[2].value);
    }
}
