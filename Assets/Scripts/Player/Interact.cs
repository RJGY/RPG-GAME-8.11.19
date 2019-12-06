using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Player
{
    public class Interact : MonoBehaviour
    {
        private LinearInventory inventory;
        private KeyCode interactKey;
        // Start is called before the first frame update
        void Start()
        {
            inventory = FindObjectOfType<LinearInventory>();
            AssignKey();
        }

        public void AssignKey()
        {
            foreach (KeyValuePair<string, KeyCode> key in KeyBindings.keys)
            {
                if (key.Key.Equals("Interact"))
                {
                    interactKey = key.Value;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(interactKey))
            {
                Ray interactionRay;
                interactionRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hitInfo;

                if (Physics.Raycast(interactionRay, out hitInfo, 10))
                {
                    switch (hitInfo.collider.tag)
                    {
                        case "NPC":
                            DialogueOption dialogue = hitInfo.transform.GetComponent<DialogueOption>();
                            if (dialogue != null)
                            {
                                dialogue.showDialogue = true;
                                Time.timeScale = 0;
                                Cursor.visible = true;
                                Cursor.lockState = CursorLockMode.None;
                                Debug.Log("Talked to an NPC");
                            }
                            else
                            {
                                Debug.Log("Tried to talk to an NPC. No dialogue attached or you dun fucked up.");
                            }
                            break;

                        case "Item":
                            Debug.Log("Pick up an Item.");
                            ItemHandler handler = hitInfo.transform.GetComponent<ItemHandler>();
                            if (handler != null)
                            {
                                handler.OnCollection();
                            }
                            break;
                        case "Chest":
                            Debug.Log("Open the chest");
                            Chest chest = hitInfo.transform.GetComponent<Chest>();
                            if (chest != null)
                            {
                                chest.showChest = true;
                                LinearInventory.showInventory = true;
                            }
                            break;
                        case "Shop":
                            Debug.Log("Open the shop");
                            Shop shop = hitInfo.transform.GetComponent<Shop>();
                            if (shop != null)
                            {
                                shop.showShop = true;
                                LinearInventory.showInventory = true;
                            }
                            break;

                    }
                }
            }
        }
    }
}