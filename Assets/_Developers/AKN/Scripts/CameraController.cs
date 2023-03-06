using Poop.Manager;
using UnityEngine;

namespace Poop.Player
{
    public class CameraController : MonoBehaviour
    {
        private InputManager inputManager;

        [SerializeField] private Transform target;
        [SerializeField] private float topClamp = 70.0f;
        [SerializeField] private float bottomClamp = -30.0f;

        #region Cached Variables
        private float cinemachineTargetY;
        private float cinemachineTargetX;
        #endregion

        private void Start()
        {
            inputManager = InputManager.Instance;
            cinemachineTargetY = target.transform.rotation.eulerAngles.y;
        }

        private void LateUpdate()
        {
            HandleRotate();
        }

        private void HandleRotate()
        {
            //Don't multiply mouse input by Time.deltaTime;
            //float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;

            cinemachineTargetY += inputManager.Look.x * deltaTimeMultiplier;
            cinemachineTargetX += inputManager.Look.y * deltaTimeMultiplier;

            cinemachineTargetY = ClampAngle(cinemachineTargetY, float.MinValue, float.MaxValue);
            cinemachineTargetX = ClampAngle(cinemachineTargetX, bottomClamp, topClamp);

            target.transform.rotation = Quaternion.Euler(cinemachineTargetX, cinemachineTargetY, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
