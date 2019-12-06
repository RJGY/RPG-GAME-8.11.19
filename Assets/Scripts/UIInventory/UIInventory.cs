using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.Player
{
    public class UIInventory : MonoBehaviour
    {
        public static List<Item> inventory = new List<Item>();
        public static List<Button> parrallelButtonList = new List<Button>();
        public bool showInventory = false;
        public Item selectedItem;
        public Image selectedItemImage;
        public Text selectedItemTitle;
        public Text selectedItemDescription;
        public Button selectedItemDropButton;
        public Button selectedItemUseButton;
        public GameObject selectedItemBackground;
        public Dropdown dropdownFilter;

        public Vector2 screen;
        private bool isGamePaused = false;
        public bool inFilterOption;
        public GameObject buttonPrefab;
        public GameObject gridLayout;
        public Scrollbar scrollbar;
        private Vector3 origin;
        public static int money;
        public string sortType = "All";
        public Transform dropLocation;
        private PauseMenu pauseMenu;
        private KeyCode inventoryKey;

        public EquippedItems[] equippedItems;
        [System.Serializable]
        public struct EquippedItems
        {
            public string name;
            public Transform location;
            public GameObject equippedItem;
        }

        // Start is called before the first frame update
        void Start()
        {
            // Setting objects inactive on start.
            scrollbar.gameObject.SetActive(false);
            // Adding listeners to objects
            dropdownFilter.onValueChanged.AddListener(delegate { Filter(dropdownFilter.value); });
            scrollbar.onValueChanged.AddListener((float value) => Scroll(value));
            selectedItemImage.gameObject.SetActive(false);
            selectedItemDescription.gameObject.SetActive(false);
            selectedItemTitle.gameObject.SetActive(false);
            selectedItemUseButton.onClick.AddListener(UseItem);
            selectedItemDropButton.onClick.AddListener(DropItem);
            selectedItemUseButton.GetComponentInChildren<Text>().text = "Use/Equip";
            selectedItemDropButton.GetComponentInChildren<Text>().text = "Drop";
            selectedItemUseButton.gameObject.SetActive(false);
            selectedItemDropButton.gameObject.SetActive(false);
            selectedItemBackground.SetActive(false);
            dropdownFilter.gameObject.SetActive(false);

            origin = gridLayout.transform.position;
            screen = new Vector2(Screen.width / 16, Screen.height / 9);
            pauseMenu = FindObjectOfType<PauseMenu>();
            // Adding events to pausing.
            pauseMenu.PausedMenu += PauseMenu_PausedMenu;

            // Creating item in inventory
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

            // Creates button elements.
            CreateButtonInventory();
            // Sets key to player prefs.
            inventoryKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "Tab"));
        }

        // If we pause, set a bool in our script.
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

        // Assigning keys ingame in case we edit them ingame.
        public void AssignKey()
        {
            foreach (KeyValuePair<string, KeyCode> key in KeyBindings.keys)
            {
                if (key.Key.Equals("Inventory"))
                {
                    inventoryKey = key.Value;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(inventoryKey) && !isGamePaused)
            {
                // Toggles inventory.
                showInventory = !showInventory;

                // If we are showing the inventory, pause the game.
                if (showInventory)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    ShowInventory();
                    EnableScrollBar();
                }
                // Otherwise we dont.
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    selectedItem = null;
                    CloseInventory();
                    DisableScrollBar();
                }
            }

            // If we pause the game whilst in the inventory, close the inventory.
            if (isGamePaused)
            {
                CloseInventory();
            }
            // Debugging code.
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

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                foreach (Item item in inventory)
                {
                    Debug.Log(item.Name);
                }
            }
        }

        // If the game for some reason has the inventory open but time is sitll running, repause the game.
        private void RepauseGame()
        {
            if (showInventory && Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }

        // Filter items in inventory.
        private void Filter(int value)
        {
            switch (value)
            {
                // Sets all to active and lists all items.
                case 0:
                    {
                        foreach (Button button in parrallelButtonList)
                        {
                            button.gameObject.SetActive(true);
                        }
                    }
                    break;
                // Sets only Armour items active.
                case 1:
                    {
                        for (int i = 0; i < inventory.Count; i++)
                        {
                            if (inventory[i].Type == ItemTypes.Armour)
                            {
                                parrallelButtonList[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                parrallelButtonList[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;
                    // Didnt bother making more filters cause its tedious.
                default:
                    break;
            }
        }
        // Counts items in inventory because list.Count doesnt like me.
        private int CountItems()
        {
            int count = 0;
            foreach (Item item in inventory)
            {
                count++;
            }
            return count;
        }

        // Sets scrollbar size up.
        private void ScrollBarSetup()
        {
            scrollbar.size = 18f / (float)CountItems();
        }
        
        // When the scrollbar is moved, move the inventory as well.
        private void Scroll(float value)
        {
            int scrollCapacity = (CountItems() - 18) * 30;
            gridLayout.transform.position = origin + new Vector3(0, scrollCapacity * value, 0);
        }

        // Enables scrollbar in the inventory.
        private void EnableScrollBar()
        {
            if (CountItems() > 18)
            {
                ScrollBarSetup();
                scrollbar.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Cannot enable scrollbar. Not enough items");
            }
        }

        // Disables scrollbar.
        private void DisableScrollBar()
        {
            scrollbar.gameObject.SetActive(false);
        }

        // Creates button inventory 
        private void CreateButtonInventory()
        {
            // For each item currently in the inventory, we create a button.
            foreach (Item item in inventory)
            {
                // Instantiates button from a prefab.
                Button button = Instantiate(buttonPrefab, gridLayout.transform).GetComponent<Button>();
                // Sets the text of the button to the name of the item.
                button.GetComponentInChildren<Text>().text = item.Name;
                // Adds the butotn to a list.
                parrallelButtonList.Add(button);
                // Sets the button inactive for the moment because this is called during start.
                button.gameObject.SetActive(false);
            }
        }

        // Used in game if the player were to pick up an item while the game is running.
        public void AddButtonInventory(Item item)
        {
            // Same as CreateButtonInventory.
            inventory.Add(item);
            Button button = Instantiate(buttonPrefab, gridLayout.transform).GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = item.Name;
            parrallelButtonList.Add(button);
            if (showInventory)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
            // Redes the scrollbar to make sure it is in sync.
            ScrollBarSetup();
        }

        // Shows the inventory.
        private void ShowInventory()
        {
            foreach (Button button in parrallelButtonList)
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(delegate { SelectItem(button); });
            }
            dropdownFilter.gameObject.SetActive(true);
        }

        // Selects item and blows up an image, description and the number of items we have.
        private void SelectItem(Button button)
        {
            // Sorts through each item to find the item we clicked.
            foreach (Item item in inventory)
            {
                if (button.GetComponentInChildren<Text>().text == item.Name)
                {
                    selectedItem = item;
                    break;
                }
            }

            // Blows up image, text and number of items.
            selectedItemImage.gameObject.SetActive(true);
            selectedItemImage.sprite = Sprite.Create(selectedItem.IconName, new Rect(0, 0, selectedItem.IconName.width, selectedItem.IconName.height), new Vector2(0, 0));
            selectedItemTitle.gameObject.SetActive(true);
            selectedItemTitle.text = selectedItem.Name;
            selectedItemDescription.gameObject.SetActive(true);
            selectedItemDescription.text = "Amount: " + selectedItem.Amount + "\n\n" + selectedItem.Description;
            selectedItemDropButton.gameObject.SetActive(true);
            selectedItemUseButton.gameObject.SetActive(true);
            selectedItemBackground.SetActive(true);
        }
        // Use item once we have selected the item from our inventory.
        void UseItem()
        {
            // Depending on the type of the item, do something different.
            switch (selectedItem.Type)
            {
                case ItemTypes.Armour:

                    break;
                case ItemTypes.Weapon:
                    // In the case of weapons we can equip or unequip it.
                    if (equippedItems[1].equippedItem == null || selectedItem.Name != equippedItems[1].name)
                    {
                        // Equip
                        if (equippedItems[1].equippedItem != null)
                        {
                            Destroy(equippedItems[1].equippedItem);
                        }
                        equippedItems[1].equippedItem = Instantiate(selectedItem.MeshName, equippedItems[1].location);
                        equippedItems[1].name = selectedItem.Name;
                    }
                    else
                    {
                        // Unequip
                        Destroy(equippedItems[1].equippedItem);
                        equippedItems[1].equippedItem = null;
                        equippedItems[1].name = "";
                    }
                    break;
                case ItemTypes.Potion:

                    break;
                case ItemTypes.Food:

                    break;
                case ItemTypes.Ingredient:

                    break;
                case ItemTypes.Craftable:

                    break;
                case ItemTypes.Material:

                    break;
                default:
                    break;
            }
        }

        // Drop an item once we hvae selected it. CURRENTLY ONLY WORKS FOR THE AXE CAUSE THATS THE ONLY THING THAT HAS A MESH.
        // CURRENTLY ONLY WORKS FOR THE AXE CAUSE THATS THE ONLY THING THAT HAS A MESH.
        void DropItem()
        {
            Debug.Log("Dropped " + selectedItem.Name);
            // Check if the item is equipped.
            for (int i = 0; i < equippedItems.Length; i++)
            {
                if (equippedItems[i].equippedItem != null && selectedItem.Name == equippedItems[i].name)
                {
                    Destroy(equippedItems[i].equippedItem);
                }
            }

            // If item is equipped, remove equipment

            GameObject itemToDrop = Instantiate(selectedItem.MeshName, dropLocation.position, Quaternion.identity);
            // Spawn item at drop location
            itemToDrop.name = selectedItem.Name;
            itemToDrop.AddComponent<Rigidbody>().useGravity = true;

            // if have stacks of it
            if (selectedItem.Amount > 1)
            {
                // just remove one stack of it
                selectedItem.Amount--;
            }
            else
            {
                // Delete it
                RemoveItemAndButton(selectedItem);
                selectedItem = null;
                return;
            }
        }

        // Removes the item and corresponding button from the inventory.
        private void RemoveItemAndButton(Item item)
        {
            // creates a button data
            Button buttonToBeRemoved = null;
            // Removes item from inventory.
            inventory.Remove(item);
            foreach (Button button in parrallelButtonList)
            {
                if (item.Name == button.GetComponentInChildren<Text>().text)
                {
                    buttonToBeRemoved = button;
                }
            }
            parrallelButtonList.Remove(buttonToBeRemoved);
            Destroy(buttonToBeRemoved.gameObject);
            CloseSelectedItem();
            ScrollBarSetup();
            EnableScrollBar();
            // Cleans up the scroll bar.
        }

        // Closes inventory.
        private void CloseInventory()
        {
            foreach (Button button in parrallelButtonList)
            {
                button.gameObject.SetActive(false);
            }
            dropdownFilter.gameObject.SetActive(false);
            CloseSelectedItem();
        }

        // Closes the selected item information including image, text, number of items, etc.
        void CloseSelectedItem()
        {
            selectedItem = null;

            scrollbar.gameObject.SetActive(false);
            selectedItemImage.gameObject.SetActive(false);
            selectedItemImage.sprite = null;
            selectedItemTitle.gameObject.SetActive(false);
            selectedItemTitle.text = "";
            selectedItemDescription.gameObject.SetActive(false);
            selectedItemDescription.text = "";
            selectedItemDropButton.gameObject.SetActive(false);
            selectedItemUseButton.gameObject.SetActive(false);
            selectedItemBackground.SetActive(false);
        }
    }
}
