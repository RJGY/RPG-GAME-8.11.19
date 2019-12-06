using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Player
{
    public class ItemHandler : MonoBehaviour
    {
        public int itemID;
        public ItemTypes itemType;
        public int amount = 1;
        public LinearInventory linearInventory;
        public UIInventory uIInventory;
        private void Start()
        {
            linearInventory = FindObjectOfType<LinearInventory>();
            uIInventory = FindObjectOfType<UIInventory>();
        }
        public void OnCollection()
        {
            if (linearInventory != null)
            {
                Debug.Log("Linear Inventory Collection");
                if (ItemTypes.Money == itemType)
                {
                    // Money
                    LinearInventory.money += amount;
                }
                else if (itemType == ItemTypes.Craftable || itemType == ItemTypes.Food || itemType == ItemTypes.Potion || itemType == ItemTypes.Ingredient)
                {
                    // Is Stackable
                    bool found = false;
                    int addIndex = -1;
                    for (int i = 0; i < LinearInventory.inventory.Count; i++)
                    {
                        if (itemID == LinearInventory.inventory[i].ID)
                        {
                            found = true;
                            addIndex = i;
                            break;
                        }
                    }

                    if (found)
                    {
                        LinearInventory.inventory[addIndex].Amount += amount;
                    }
                    else
                    {
                        LinearInventory.inventory.Add(ItemData.CreateItem(itemID));
                        if (amount > 1)
                        {
                            for (int i = 0; i < LinearInventory.inventory.Count; i++)
                            {
                                if (itemID == LinearInventory.inventory[i].ID)
                                {
                                    LinearInventory.inventory[i].Amount = amount;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    LinearInventory.inventory.Add(ItemData.CreateItem(itemID));
                }

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("UIInventory Collection");
                if (ItemTypes.Money == itemType)
                {
                    // Money
                    UIInventory.money += amount;
                }
                else if (itemType == ItemTypes.Craftable || itemType == ItemTypes.Food || itemType == ItemTypes.Potion || itemType == ItemTypes.Ingredient)
                {
                    // Is Stackable
                    bool found = false;
                    foreach (Item item in UIInventory.inventory)
                    {
                        if (itemID == item.ID)
                        {
                            found = true;
                            item.Amount += amount;
                            break;
                        }
                    }

                    if (!found)
                    {
                        uIInventory.AddButtonInventory(ItemData.CreateItem(itemID));
                        if (amount > 1)
                        {
                            for (int i = 0; i < UIInventory.inventory.Count; i++)
                            {
                                if (itemID == UIInventory.inventory[i].ID)
                                {
                                    UIInventory.inventory[i].Amount = amount;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    uIInventory.AddButtonInventory(ItemData.CreateItem(itemID));
                }

                Destroy(gameObject);
            }
        }
    }
}
