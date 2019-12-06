using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Player
{
    public class KeyBindings : MonoBehaviour
    {
        public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
        public static Dictionary<string, Text> keytexts = new Dictionary<string, Text>();
        public KeyCode tempKey;
        public Text forwardButton, backwardButton, leftButton, rightButton, jumpButton, inventoryButton, cancelButton, interactButton, sprintButton, crouchButton;
        private Movement player;
        private UIInventory inventory;
        private Interact interact;
        private PauseMenu pause;
        public bool inMenu;
        private void Awake()
        {
            // Adds each key currently in player prefs or set to deault to a dictionary.
            // Adds each text element to a dictionary.

            keys.Add("Forward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")));
            keytexts.Add("Forward", forwardButton);
            keys.Add("Backward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S")));
            keytexts.Add("Backward", backwardButton);
            keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
            keytexts.Add("Left", leftButton);
            keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
            keytexts.Add("Right", rightButton);
            keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
            keytexts.Add("Jump", jumpButton);
            keys.Add("Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "Tab")));
            keytexts.Add("Inventory", inventoryButton);
            keys.Add("Cancel", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Cancel", "Escape")));
            keytexts.Add("Cancel", cancelButton);
            keys.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E")));
            keytexts.Add("Interact", interactButton);
            keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
            keytexts.Add("Sprint", sprintButton);
            keys.Add("Crouch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl")));
            keytexts.Add("Crouch", crouchButton);

            // Sets each text element to the corresponding keybind.
            foreach (KeyValuePair<string, Text> item in keytexts)
            {
                foreach (KeyValuePair<string, KeyCode> keyValuePair in keys)
                {
                    if (item.Key.Equals(keyValuePair.Key))
                    {
                        item.Value.text = keyValuePair.Value.ToString();
                        break;
                    }
                }
            }

            if (!inMenu)
            {
                // Find objects incase we need them (only when in game)
                player = FindObjectOfType<Movement>();
                inventory = FindObjectOfType<UIInventory>();
                interact = FindObjectOfType<Interact>();
                pause = FindObjectOfType<PauseMenu>();
            }

            // Sets gameobject inactive after completing awake.
            gameObject.SetActive(false);
        }

        public void Apply()
        {
            // If in menu, dont assign keys as they dont exist in the menu
            if (!inMenu)
            {
                player.AssignKeys();
                inventory.AssignKey();
                interact.AssignKey();
                pause.AssignKey();
            }

            // Save each key to player prefs
            foreach (KeyValuePair<string, KeyCode> key in keys)
            {
                PlayerPrefs.SetString(key.Key, key.Value.ToString());
            }
        }

        // Specific functions for keys.

        public void Forward()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code that we are editting a key to make it so we cannot edit this key.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Forward"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Forward");
                keys.Add("Forward", KeyCode.None);
                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Forward"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }

            }
        }

        public void Backward()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Backward"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Backward");
                keys.Add("Backward", KeyCode.None);
                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Backward"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }
        }

        public void Left()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Left"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Left");
                keys.Add("Left", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Left"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }


        }

        public void Right()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Right"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Right");
                keys.Add("Right", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Right"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }


        }

        public void Jump()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Jump"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Jump");
                keys.Add("Jump", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Jump"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }


        }

        public void Inventory()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Inventory"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Inventory");
                keys.Add("Inventory", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Inventory"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }


        }

        public void Cancel()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Cancel"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Cancel");
                keys.Add("Cancel", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Cancel"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }


        }

        public void Interact()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Interact"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Interact");
                keys.Add("Interact", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Interact"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }


        }

        public void Sprint()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Sprint"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Sprint");
                keys.Add("Sprint", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Sprint"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }


        }

        public void Crouch()
        {
            bool triggered = false;
            foreach (KeyCode key in keys.Values)
            {
                // If we are editting another key,
                if (key == KeyCode.None)
                {
                    // Set a bool to tell the code.
                    triggered = true;
                }
            }
            if (!triggered)
            {

                foreach (KeyValuePair<string, KeyCode> item in keys)
                {
                    if (item.Key.Equals("Crouch"))
                    {
                        tempKey = item.Value;
                        break;
                    }
                }

                // If the bool wasnt triggered, we take the key and remember it and set it to none.
                keys.Remove("Crouch");
                keys.Add("Crouch", KeyCode.None);

                // We always set the text to the corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals("Crouch"))
                    {
                        item.Value.text = KeyCode.None.ToString();
                        break;
                    }
                }
            }
        }

        private void ApplySettings()
        {
            // Loop through all keys
            foreach (KeyCode key in keys.Values)
            {
                // If any keys is not set, get out cause they should be able to exit.
                if (key == KeyCode.None)
                {
                    return;
                }
            }
            foreach (KeyValuePair<string, KeyCode> item in keys)
            {

                // Horizontal alt keys
                // vertical alt keys
            }
        }

        private void OnGUI()
        {
            // Saves current keypress
            Event e = Event.current;
            bool trigger = false;
            string valueToBeChanged = null;
            // Loop through each key in the keys
            foreach (KeyValuePair<string, KeyCode> item in keys)
            {
                // Filter to find key which is set to none
                if (item.Value == KeyCode.None)
                {
                    // Set value to remember the key we are changing
                    valueToBeChanged = item.Key;
                    // Loop through each key again.
                    foreach (KeyCode key in keys.Values)
                    {
                        // Make sure we are not setting the key to another keybind.
                        if (e.keyCode == key && e.keyCode != KeyCode.None)
                        {
                            // If we are setting the key to another keybind, exit loop and raise flag
                            trigger = true;
                            break;
                        }
                    }
                    break;
                }
            }
            // If no flags raised
            if (!trigger)
            {
                // If no value is being changed, don't do anything.
                if (valueToBeChanged == null)
                {
                    return;
                }
                // Change keys
                keys.Remove(valueToBeChanged);
                keys.Add(valueToBeChanged, e.keyCode);
                // Change text to corresponding key.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals(valueToBeChanged))
                    {
                        item.Value.text = e.keyCode.ToString();
                        break;
                    }
                }
            }
            // If flag raised and trying to set keybind of something else,
            else
            {
                // Exit function because nothing is happening.
                if (valueToBeChanged == null)
                {
                    return;
                }

                // Change keys back to temp values.
                keys.Remove(valueToBeChanged);
                keys.Add(valueToBeChanged, tempKey);
                // Change text back to normal.
                foreach (KeyValuePair<string, Text> item in keytexts)
                {
                    if (item.Key.Equals(valueToBeChanged))
                    {
                        item.Value.text = tempKey.ToString();
                        break;
                    }
                }
            }
        }

        public void DeleteDictionaries()
        {
            keys.Clear();
            keytexts.Clear();
        }
    }
}
