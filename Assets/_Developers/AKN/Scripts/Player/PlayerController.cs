using Poop.Manager;
using Poop.Player.Inventory;
using System;
using UnityEngine;

namespace Poop.Player
{
    public enum PlayerType
    {
        None = 0,
        Student = 1,
        Principal = 2,
    }

    public enum PlayerState
    {
        None = 0,
        DoingTask = 1,
    }

    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public InventoryController InventoryController { get; private set; }

        [SerializeField] private Item highlightedItem;
        public event EventHandler<OnHighlightedItemChangedEventArgs> OnHighlightedItemChanged;
        public class OnHighlightedItemChangedEventArgs : EventArgs
        {
            public Item HighlightedItem;
        }

        [SerializeField] private PlayerType playerType;
        [SerializeField] private PlayerState playerState;

        private CharacterController characterController;

        [Tooltip("Student = 1, Principal = 1.7")]
        [SerializeField] private float walkSpeed = 0.0f;
        [Tooltip("Student = 2.8, Principal = 3")]
        [SerializeField] private float runSpeed = 0.0f;

        [SerializeField] private LayerMask interactableMask;
        [SerializeField] private float interactDistance = 6f;

        #region Constant Variables
        private const float Gravity = -9.81f;
        private const float SpeedChangeRate = 10.0f;
        private const float SpeedOffset = 0.1f;
        private const float InputMagnitude = 1.0f;
        private const float RotationSmoothness = 0.1f;
        #endregion

        #region Cached Variables
        private float speed;
        private float targetRotation;
        private float rotationVelocity;
        #endregion

        private void Awake()
        {
            Instance = this;

            if (walkSpeed == 0.0f || runSpeed == 0.0f) Debug.LogWarning("Walk and run speed are not set.");

            characterController = GetComponent<CharacterController>();
            InventoryController = GetComponent<InventoryController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Start()
        {
            InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
            InputManager.Instance.OnDropAction += InputManager_OnDropAction;
        }

        private void InputManager_OnDropAction(object sender, EventArgs e)
        {
            if (!InventoryController.GetItemInHand()) return;

            InventoryController.DropItem();
        }

        private void InputManager_OnInteractAction(object sender, EventArgs e)
        {
            if (highlightedItem == null) return;

            highlightedItem.Interact();
            SetHighlightedItem(null);
        }

        private void Update()
        {
            HandleMove();
            HandleRotate();
            HandleInteraction();
        }

        private void HandleMove()
        {
            float targetSpeed = InputManager.Instance.Run ? runSpeed : walkSpeed;
            if (InputManager.Instance.Move == Vector2.zero) targetSpeed = 0.0f;

            AccelerateSpeed(targetSpeed);

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, Gravity, 0.0f) * Time.deltaTime);
        }

        private void HandleRotate()
        {
            if (InputManager.Instance.Move == Vector2.zero) return;

            Vector3 lookDirection = new Vector3(InputManager.Instance.Move.x, 0.0f, InputManager.Instance.Move.y).normalized;

            targetRotation = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothness);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        private void HandleInteraction()
        {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableMask);
                Item hitItem = hit.collider?.GetComponent<Item>();

                if (hitItem != highlightedItem && hitItem != InventoryController.GetItemInHand() && playerType == PlayerType.Student)
                {
                    SetHighlightedItem(hitItem);
                }
                else if (!hitSomething)
                {
                    SetHighlightedItem(null);
                }
        }

        private void AccelerateSpeed(float targetSpeed)
        {
            float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

            if (currentHorizontalSpeed < targetSpeed - SpeedOffset || currentHorizontalSpeed > targetSpeed + SpeedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * InputMagnitude, Time.deltaTime * SpeedChangeRate);
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }
        }

        public float GetMoveAmount()
        {
            float horizontalInput = InputManager.Instance.Move.x;
            float verticalInput = InputManager.Instance.Move.y;

            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            if (InputManager.Instance.Run) return moveAmount *= 2f;
            else return moveAmount;
        }

        public PlayerType GetPlayerType()
        {
            return playerType;
        }

        private void SetHighlightedItem(Item highlightedItem)
        {
            this.highlightedItem = highlightedItem;

            OnHighlightedItemChanged?.Invoke(this, new OnHighlightedItemChangedEventArgs
            {
                HighlightedItem = highlightedItem
            });
        }
    }
}