using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Player
{
    public class LinearInventory : MonoBehaviour
    {
        #region Variables
        public static List<Item> inventory = new List<Item>();
        public static bool showInventory = false;
        public Item item;
        public Item selectedItem;
        public Vector2 screen;
        private bool isGamePaused = false;
        public bool inFilterOption;
        public GUIStyle titleStyle;
        public GUISkin invSkin;


        public static int money;
        public Vector2 scrollPosition;
        public string sortType = "All";
        public Transform dropLocation;
        private PauseMenu pauseMenu;
        public EquippedItems[] equippedItems;
        [System.Serializable]
        public struct EquippedItems
        {
            public string name;
            public Transform location;
            public GameObject equippedItem;
        }
        #endregion

        private void Start()
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
            pauseMenu.PausedMenu += PauseMenu_PausedMenu;

            inventory.Add(ItemData.CreateItem(0));
            inventory.Add(ItemData.CreateItem(1));
            inventory.Add(ItemData.CreateItem(2));

            inventory.Add(ItemData.CreateItem(100));
            inventory.Add(ItemData.CreateItem(101));
            inventory.Add(ItemData.CreateItem(102));

            inventory.Add(ItemData.CreateItem(200));
            inventory.Add(ItemData.CreateItem(201));
            inventory.Add(ItemData.CreateItem(202));

            inventory.Add(ItemData.CreateItem(300));
            inventory.Add(ItemData.CreateItem(301));

            inventory.Add(ItemData.CreateItem(400));
            inventory.Add(ItemData.CreateItem(401));
            inventory.Add(ItemData.CreateItem(402));

            inventory.Add(ItemData.CreateItem(500));
            inventory.Add(ItemData.CreateItem(501));
            inventory.Add(ItemData.CreateItem(502));

            inventory.Add(ItemData.CreateItem(600));
            inventory.Add(ItemData.CreateItem(601));
            inventory.Add(ItemData.CreateItem(602));

            inventory.Add(ItemData.CreateItem(700));


        }

        private void PauseMenu_PausedMenu(bool obj)
        {
            if (obj)
            {
                isGamePaused = true;
            }
            else
            {
                isGamePaused = false;
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Inventory") && !isGamePaused)
            {
                showInventory = !showInventory;

                if (showInventory)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    selectedItem = null;
                }
            }

            if (showInventory)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }


            if (Input.GetKey(KeyCode.I))
            {
                inventory.Add(ItemData.CreateItem(Random.Range(0, 3)));
            }

            if (Input.GetKey(KeyCode.Keypad1))
            {
                if (selectedItem != null)
                {
                    selectedItem.Amount++;
                }
            }

            if (Input.GetKey(KeyCode.Keypad2))
            {
                money++;
            }
        }

        private void RepauseGame()
        {
            if (showInventory && Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }

        private void OnGUI()
        {
            if (showInventory && !isGamePaused)
            {
                screen = new Vector2(Screen.width / 16, Screen.height / 9);
                GUI.skin = invSkin;
                DisplayInventory();
                RepauseGame();

                // Filter
                if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 6.5f, screen.x, screen.y * 0.25f), "Filter", titleStyle))
                {
                    inFilterOption = !inFilterOption;

                }

                if (inFilterOption)
                {
                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 6.75f, screen.x, screen.y * 0.25f), "All"))
                    {
                        selectedItem = null;
                        sortType = "All";
                    }

                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 7f, screen.x, screen.y * 0.25f), "Armour"))
                    {
                        selectedItem = null;
                        sortType = "Armour";
                    }

                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 7.25f, screen.x, screen.y * 0.25f), "Weapon"))
                    {
                        selectedItem = null;
                        sortType = "Weapon";
                    }

                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 7.5f, screen.x, screen.y * 0.25f), "Potion"))
                    {
                        selectedItem = null;
                        sortType = "Potion";
                    }

                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 7.75f, screen.x, screen.y * 0.25f), "Food"))
                    {
                        selectedItem = null;
                        sortType = "Food";
                    }

                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 8f, screen.x, screen.y * 0.25f), "Ingredient"))
                    {
                        selectedItem = null;
                        sortType = "Ingredient";
                    }

                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 8.25f, screen.x, screen.y * 0.25f), "Quest"))
                    {
                        selectedItem = null;
                        sortType = "Quest";
                    }

                    if (GUI.Button(new Rect(screen.x * 14.5f, screen.y * 8.5f, screen.x, screen.y * 0.25f), "Misc"))
                    {
                        selectedItem = null;
                        sortType = "Misc";
                    }
                }

                GUI.skin = null;

                if (selectedItem == null)
                {
                    return;
                }

                else
                {
                    UseItem();
                }
            }
        }

        private void DisplayInventory()
        {
            if (!(sortType == "All" || sortType == ""))
            {
                // Specific item filter.
                ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);

                int a = 0;
                int s = 0;

                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].Type == type)
                    {
                        a++;
                    }
                }

                if (a <= 34)
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (inventory[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * screen.x, screen.y * 0.25f + s * (screen.y * 0.25f), 3 * screen.x, 0.25f * screen.y), inventory[i].Name))
                            {
                                selectedItem = inventory[i];
                            }

                            s++;
                        }
                    }
                }
                else
                {
                    scrollPosition = GUI.BeginScrollView(new Rect(0, 0.25f * screen.y, 3.75f * screen.x, 8.5f * screen.y), scrollPosition, new Rect(0, 0, 0, a * (0.25f * screen.y)), false, true);

                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (inventory[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * screen.x, s * (screen.y * 0.25f), 3 * screen.x, 0.25f * screen.y), inventory[i].Name))
                            {
                                selectedItem = inventory[i];
                            }
                            s++;

                        }
                    }

                    GUI.EndScrollView();
                }
            }
            else
            {
                // Show everything.
                if (inventory.Count <= 34)
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (GUI.Button(new Rect(0.5f * screen.x, screen.y * 0.25f + i * (screen.y * 0.25f), 3 * screen.x, 0.25f * screen.y), inventory[i].Name))
                        {
                            selectedItem = inventory[i];
                        }
                    }
                }

                else
                {
                    scrollPosition = GUI.BeginScrollView(new Rect(0, 0.25f * screen.y, 3.75f * screen.x, 8.5f * screen.y), scrollPosition, new Rect(0, 0, 0, inventory.Count * (0.25f * screen.y)), false, true);

                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (GUI.Button(new Rect(0.5f * screen.x, i * (screen.y * 0.25f), 3 * screen.x, 0.25f * screen.y), inventory[i].Name))
                        {
                            selectedItem = inventory[i];
                        }
                    }

                    GUI.EndScrollView();
                }
            }
        }

        private void UseItem()
        {
            GUI.Box(new Rect(screen.x * 4f, 0.25f * screen.y, screen.x * 3, screen.y * 0.5f), selectedItem.Name, titleStyle);
            GUI.Box(new Rect(screen.x * 4f, 0.75f * screen.y, screen.x * 3, screen.y * 3), selectedItem.IconName);
            GUI.Box(new Rect(screen.x * 4f, 3.75f * screen.y, screen.x * 3, screen.y * 3), selectedItem.Amount + "\n" + selectedItem.Description);
            switch (selectedItem.Type)
            {
                case ItemTypes.Armour:
                    if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Equip"))
                    {

                    }
                    break;
                case ItemTypes.Weapon:

                    if (equippedItems[1].equippedItem == null || selectedItem.Name != equippedItems[1].equippedItem.name)
                    {
                        if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Equip"))
                        {
                            if (equippedItems[1].equippedItem != null)
                            {
                                Destroy(equippedItems[1].equippedItem);
                            }
                            equippedItems[1].equippedItem = Instantiate(selectedItem.MeshName, equippedItems[1].location);
                            equippedItems[1].equippedItem.name = selectedItem.Name;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Unequip"))
                        {
                            Destroy(equippedItems[1].equippedItem);
                            equippedItems[1].equippedItem = null;
                        }
                    }
                    break;
                case ItemTypes.Potion:
                    if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Consume"))
                    {

                    }
                    break;
                case ItemTypes.Food:
                    if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Consume"))
                    {

                    }
                    break;
                case ItemTypes.Ingredient:
                    if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Craft"))
                    {

                    }
                    break;
                case ItemTypes.Craftable:
                    if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Use"))
                    {

                    }
                    break;
                case ItemTypes.Material:
                    if (GUI.Button(new Rect(screen.x * 4f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Craft"))
                    {

                    }
                    break;
                default:
                    break;
            }
            if (GUI.Button(new Rect(screen.x * 5.5f, 6.75f * screen.y, screen.x * 1.5f, screen.y * 0.5f), "Discard"))
            {
                // Check if the item is equipped.
                for (int i = 0; i < equippedItems.Length; i++)
                {
                    if (equippedItems[i].equippedItem != null && selectedItem.Name == equippedItems[i].name)
                    {
                        Destroy(equippedItems[i].equippedItem);
                    }
                }

                // If item is equipped, remove equipment

                GameObject itemToDrop = Instantiate(selectedItem.MeshName, dropLocation.position, Quaternion.identity, null);
                // Spawn item at drop location
                itemToDrop.name = selectedItem.Name;
                itemToDrop.AddComponent<Rigidbody>().useGravity = true;

                //
                if (selectedItem.Amount > 1)
                {
                    selectedItem.Amount--;
                }
                else
                {
                    inventory.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }
    }
}
