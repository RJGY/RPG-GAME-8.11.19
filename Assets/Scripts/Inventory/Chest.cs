using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Player
{
    public class Chest : MonoBehaviour
    {
        public bool showChest;
        public List<Item> itemsInChest = new List<Item>();
        public Item selectedItem;
        public int[] itemsToSpawn;

        // Add scroll later
        private void Start()
        {
            for (int i = 0; i < itemsToSpawn.Length; i++)
            {
                itemsInChest.Add(ItemData.CreateItem(itemsToSpawn[i]));
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                showChest = false;
            }
        }

        private void OnGUI()
        {
            Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);

            if (showChest)
            {
                for (int i = 0; i < itemsInChest.Count; i++)
                {
                    if (GUI.Button(new Rect(12.75f * scr.x, 0.25f * scr.y + i * (scr.y * 0.25f), 3 * scr.x, 0.25f * scr.y), itemsInChest[i].Name))
                    {
                        selectedItem = itemsInChest[i];
                    }
                }

                if (selectedItem == null)
                {
                    return;
                }

                else
                {
                    if (GUI.Button(new Rect(12.5f * scr.x, 6.5f * scr.y, 1.5f * scr.x, 0.25f * scr.y), "Take"))
                    {
                        LinearInventory.inventory.Add(ItemData.CreateItem(selectedItem.ID));
                        itemsInChest.Remove(selectedItem);
                        selectedItem = null;
                    }
                }
            }
        }
    }
}
