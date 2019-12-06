﻿using UnityEngine;

namespace RPG.Player
{
    [AddComponentMenu("RPG/Player/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        //Allows us to create types 
        public enum RotationalAxis
        {
            MouseX,
            MouseY,
        }
        [Header("Rotation Variables")]
        public RotationalAxis axis = RotationalAxis.MouseX;
        // Slider 
        [Range(0,200)]
        // How fast do you want to make your camera want to turn
        public float Sensitivity = 20;
        // Stops the camera from a certain limit if looking up or down.
        private float _minY = -89.9f, _maxY = 89.9f;
        private float _rotY;

        private void Start()
        {
            // Checks if player has Rigibody and freezes the rotation on it if it exists
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
            // The cursor will go invisible when the game starts and will lock on the middle of the screen. If Esc key was pressed, the cursor will apear and will be unlocked
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // Checks if player has added a Camera Component and if so will change it so the rotational axis to the y axis
            if (GetComponent<Camera>())
            {
                axis = RotationalAxis.MouseY;
            }
        }
        void Update()
        {
            if (!PlayerHandler.isDead) // If we are alive, you can move the camera
            {
                // Checks if the axis is MouseX
                if (axis == RotationalAxis.MouseX)
                {
                    // Checks if the x axis is
                    transform.Rotate(0, Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime, 0);
                }
                else
                {
                    _rotY += Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;
                    _rotY = Mathf.Clamp(_rotY, _minY, _maxY);
                    transform.localEulerAngles = new Vector3(-_rotY, 0, 0);
                }
            }
        }
    }
}