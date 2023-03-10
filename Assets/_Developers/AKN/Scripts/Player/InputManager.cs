using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Poop.Manager
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public event EventHandler OnItemInteractAction;
        public event EventHandler OnItemDropAction;
        
        private PlayerInputActions inputActions;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }

        public bool IsProgressing { get; private set; }

        private void Awake()
        {
            Instance = this;

            inputActions = new PlayerInputActions();

            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Look.performed += OnLook;
            inputActions.Player.Run.performed += OnRun;
            inputActions.Player.Run.canceled += OnRun;

            inputActions.Player.InteractItem.performed += InteractItem_performed;
            inputActions.Player.DropItem.performed += DropItem_performed;

            inputActions.Player.InteractTask.performed += InteractTask_performed;

            inputActions.Player.InteractTask.canceled += InteractTask_canceled;
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void InteractItem_performed(InputAction.CallbackContext obj)
        {
            OnItemInteractAction?.Invoke(this, EventArgs.Empty);
        }

        private void DropItem_performed(InputAction.CallbackContext obj)
        {
            OnItemDropAction?.Invoke(this, EventArgs.Empty);
        }

        private void InteractTask_performed(InputAction.CallbackContext obj)
        {
            IsProgressing = true;
        }

        private void InteractTask_canceled(InputAction.CallbackContext obj)
        {
            IsProgressing = false;
        }
    }
}