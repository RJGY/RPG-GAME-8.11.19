using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Player
{
    public class PauseMenu : MonoBehaviour
    {
        public bool isPaused;
        public GameObject _pauseMenu;
        public event Action<bool> PausedMenu;
        private KeyCode pauseKey;

        void Start()
        {
            _pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
            _pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            AssignKey();
        }
        void Update()
        {
            if (Input.GetKeyDown(pauseKey))
            {
                TogglePause();
            }
        }

        public void AssignKey()
        {
            foreach (KeyValuePair<string, KeyCode> key in KeyBindings.keys)
            {
                if (key.Key.Equals("Cancel"))
                {
                    pauseKey = key.Value;
                }
            }
        }

        public void TogglePause()
        {
            if (isPaused)
            {
                if (PausedMenu != null)
                {
                    PausedMenu(false);
                }
                isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                _pauseMenu.SetActive(false);


            }

            else
            {
                if (PausedMenu != null)
                {
                    PausedMenu(true);
                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
                isPaused = true;
            }
        }
    }
}
