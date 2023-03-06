using UnityEngine;
using UnityEngine.InputSystem;

namespace Poop.Manager
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private PlayerInputActions inputActions;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }

        private void Awake()
        {
            Instance = this;

            inputActions = new PlayerInputActions();

            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Look.performed += OnLook;
            inputActions.Player.Run.performed += OnRun;
            inputActions.Player.Run.canceled += OnRun;
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

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
    }
}