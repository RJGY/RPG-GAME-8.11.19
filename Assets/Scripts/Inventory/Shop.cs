using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Player
{
    public class Shop : MonoBehaviour
    {
        public bool showShop;
        public List<Item> itemsInShop = new List<Item>();
        public Item selectedShopItem;
        public int[] itemsToSpawn;

        // Add scroll later
        private void Start()
        {
            for (int i = 0; i < itemsToSpawn.Length; i++)
            {
                itemsInShop.Add(ItemData.CreateItem(itemsToSpawn[i]));
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                showShop = false;
            }
        }

        private void OnGUI()
        {
            Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);


            if (showShop)
            {
                GUI.Box(new Rect(6.5f * scr.x, 0.25f * scr.y, 3 * scr.x, 0.5f * scr.y), "Money: $" + LinearInventory.money.ToString());

                for (int i = 0; i < itemsInShop.Count; i++)
                {
                    if (GUI.Button(new Rect(12.75f * scr.x, 0.25f * scr.y + i * (scr.y * 0.25f), 3 * scr.x, 0.25f * scr.y), itemsInShop[i].Name))
                    {
                        selectedShopItem = itemsInShop[i];
                    }
                }

                if (selectedShopItem == null)
                {
                    return;
                }

                else
                {
                    float newPrice = (Mathf.Round(selectedShopItem.Value * 1.25f));
                    int newPriceInt = (int)newPrice;
                    GUI.Box(new Rect(6.5f * scr.x, 0.75f * scr.y, 3 * scr.x, 0.5f * scr.y), "Cost: $" + newPriceInt.ToString());
                    if (LinearInventory.money >= selectedShopItem.Value)
                    {
                        if (GUI.Button(new Rect(12.5f * scr.x, 6.5f * scr.y, 1.5f * scr.x, 0.25f * scr.y), "Buy"))
                        {
                            LinearInventory.inventory.Add(ItemData.CreateItem(selectedShopItem.ID));
                            itemsInShop.Remove(selectedShopItem);
                            selectedShopItem = null;
                            LinearInventory.money -= newPriceInt;
                        }
                    }
                }
            }
        }
    }
}
