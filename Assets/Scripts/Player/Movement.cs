using System.Collections.Generic;
using UnityEngine;

namespace RPG.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed Vars")]
        //Value Variables
        public float moveSpeed;
        public float runSpeed, walkSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 20;
        public bool _isRunning = false, _isCrouching = false;
        private string _lastAction;
        private float xMovement;
        private float zMovement;
        public KeyCode forward, backward, left, right, sprint, crouch, jump;

        //Struct - Contains Multiple Variables (eg...3 floats)
        private Vector3 _moveDir;

        //Reference Variable
        private CharacterController _charController;

        private void Start()
        {
            _charController = GetComponent<CharacterController>();
            AssignKeys();
        }
        private void Update()
        {
            
            CheckSpecialMove();
            Move();
            
        }

        public void AssignKeys()
        {
            foreach (KeyValuePair<string, KeyCode> key in KeyBindings.keys)
            {
                switch (key.Key)
                {
                    case "Forward":
                        forward = key.Value;
                        break;

                    case "Backward":
                        backward = key.Value;
                        break;

                    case "Left":
                        left = key.Value;
                        break;

                    case "Right":
                        right = key.Value;
                        break;

                    case "Crouch":
                        crouch = key.Value;
                        break;

                    case "Sprint":
                        sprint = key.Value;
                        break;

                    case "Jump":
                        jump = key.Value;
                        break;
                }
            }
        }

        private void CheckSpecialMove()
        {
            
            // Check the last button press
            if (Input.GetKeyDown(sprint))
            {
                _lastAction = "Sprint";
            }

            if (Input.GetKey(sprint))
            {
                _isRunning = true;
            }
            
            if (Input.GetKeyDown(crouch))
            {
                _lastAction = "Crouch";
            }
            if (Input.GetKey(crouch))
            {
                _isCrouching = true;
            }

            if (Input.GetKeyUp(sprint))
            {
                _isRunning = false;

            }

            if (Input.GetKeyUp(crouch))
            {
                _isCrouching = false;
            }
        }

        private void Move()
        {
            if (!PlayerHandler.isDead)
            {
                if (_charController.isGrounded) 
                {

                    //set speed
                    if (_isRunning)
                    {
                        moveSpeed = runSpeed;
                    }
                    if (_isCrouching && _lastAction.Equals("Crouch"))
                    {
                        moveSpeed = crouchSpeed;
                    }
                    if (!_isRunning && !_isCrouching)
                    {
                        moveSpeed = walkSpeed;
                    }

                    if (Input.GetKey(forward) && Input.GetKey(backward))
                    {
                        zMovement = 0;
                    }
                    else if (Input.GetKey(forward))
                    {
                        zMovement = 1;
                    }
                    else if (Input.GetKey(backward))
                    {
                        zMovement = -1;
                    }
                    else
                    {
                        zMovement = 0;
                    }

                    if (Input.GetKey(right) && Input.GetKey(left))
                    {
                        xMovement = 0;
                    }
                    else if (Input.GetKey(right))
                    {
                        xMovement = 1;
                    }
                    else if (Input.GetKey(left))
                    {
                        xMovement = -1;
                    }
                    else
                    {
                        xMovement = 0;
                    }

                    // move this direction based off inputs
                    _moveDir = transform.TransformDirection(new Vector3(xMovement, 0, zMovement) * moveSpeed);
                    if (Input.GetKey(jump))
                    {
                        _moveDir.y = jumpSpeed;
                    }
                }
            }
            else
            {
                _moveDir = new Vector3(Mathf.Lerp(_moveDir.x, 0, 2*Time.deltaTime), Mathf.Lerp(_moveDir.y, 0, 2 * Time.deltaTime), Mathf.Lerp(_moveDir.z, 0, 2 * Time.deltaTime));
            }
            
            // Regardless if we are grounded or not
            // apply grvity
            _moveDir.y -= _gravity * Time.deltaTime;
            //apply movement
            _charController.Move(_moveDir * Time.deltaTime);
        }
    }
}
