using Poop.Manager;
using Poop.Player.Inventory;
using UnityEngine;

namespace Poop.Player
{
    public enum PlayerType
    {
        None,
        Student,
        Principal
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerType playerType;

        private CharacterController characterController;

        [Tooltip("Student = 1, Principal = 1.7")]
        [SerializeField] private float walkSpeed = 0.0f;
        [Tooltip("Student = 2.8, Principal = 3")]
        [SerializeField] private float runSpeed = 0.0f;

        [SerializeField] private LayerMask itemLayerMask;
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

        public virtual void Start()
        {
            if (walkSpeed == 0.0f || runSpeed == 0.0f) Debug.LogWarning("Walk and run speed are not set.");

            characterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
        }

        private void InputManager_OnInteractAction(object sender, System.EventArgs e)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit raycastHit, interactDistance, itemLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out Item item))
                {
                    item.Interact();
                }
            }
            //Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
        }

        private void Update()
        {
            HandleMove();
            HandleRotate();
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
    }
}