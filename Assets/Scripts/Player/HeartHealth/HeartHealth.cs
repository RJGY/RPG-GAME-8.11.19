using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(PlayerHandler))]

public class HeartHealth : MonoBehaviour
{

    [Header("Player Statistics")]
    [SerializeField]
    private float currentHealth, maxHealth;

    [Header("Heart Slots")]
    public Image[] heartSlots;
    public Sprite[] heartSprite;
    private float healthPerSection;

    [Header("PlayerHandler")]
    [SerializeField]
    private PlayerHandler _playerHandler;
    private void Start()
    {

        // Calculate the health points per heart section.
        _playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();

        healthPerSection = maxHealth / (heartSlots.Length * 4);
    }

    private void Update()
    {
        currentHealth = _playerHandler.currentHealth;
        UpdateHeart();
    }

    void UpdateHeart()
    {
        // Index variable starting at 0 for our slot check.
        // For all the hearts we have:
        for (int i = 0; i < heartSlots.Length; i++)
        {
            // If our health is greater or equal to the slot amount.
            if (currentHealth >= (healthPerSection * 4) + healthPerSection * 4 * i)
            {
                heartSlots[i].sprite = heartSprite[0]; // Shows player with full hearts
            }

            else if (currentHealth >= (healthPerSection * 3) + healthPerSection * 4 * i)
            {
                heartSlots[i].sprite = heartSprite[1]; // Shows player with 3 quarters of a heart
            }

            else if (currentHealth >= (healthPerSection * 2) + healthPerSection * 4 * i)
            {
                heartSlots[i].sprite = heartSprite[2]; // Shows player with half a heart
            }

            else if (currentHealth >= (healthPerSection * 1) + healthPerSection * 4 * i)
            {
                heartSlots[i].sprite = heartSprite[3]; // Shows player with a quarter of a heart
            }

            else
            {
                // You are supposed to be dead here

                heartSlots[i].sprite = heartSprite[4]; // Shows player with no hearts and therefore dead.
            }
        }
    }
}

