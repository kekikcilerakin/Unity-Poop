using Poop.Manager;
using UnityEngine;

namespace Poop.Player
{
    public class PlayerController : MonoBehaviour
    {
        private InputManager inputManager;
        private Rigidbody rb;

        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float runSpeed = 3f;
        [SerializeField] private float rotationSpeed = 15f;

        private void Start()
        {
            inputManager = InputManager.Instance;
            rb = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        private void FixedUpdate()
        {
            HandleMove();
            HandleRotation();
        }

        private void HandleMove()
        {
            float targetSpeed = inputManager.Run ? runSpeed : walkSpeed;

            Vector3 moveVelocity = GetTargetDirection() * targetSpeed;
            rb.velocity = moveVelocity;
        }

        private void HandleRotation()
        {
            Vector3 targetDirection = GetTargetDirection();

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        private Vector3 GetTargetDirection()
        {
            Vector3 targetDirection = Camera.main.transform.right * inputManager.Move.x;
            targetDirection += Camera.main.transform.forward * inputManager.Move.y;
            targetDirection.y = 0;

            return targetDirection;
        }

        public float GetMoveAmount()
        {
            float horizontalInput = inputManager.Move.x;
            float verticalInput = inputManager.Move.y;
            
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            if (inputManager.Run) return moveAmount *= 2f;
            else return moveAmount;
        }
    }
}