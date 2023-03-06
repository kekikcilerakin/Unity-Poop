using Poop.Manager;
using UnityEngine;

namespace Poop.Player
{
    public class CameraController : MonoBehaviour
    {
        private InputManager inputManager;

        [SerializeField] private Transform targetTransform;
        [SerializeField] private float TopClamp = 70.0f;
        [SerializeField] private float BottomClamp = -30.0f;

        [SerializeField] private float CameraAngleOverride = 0.0f;

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private const float _threshold = 0.01f;

        private void Start()
        {
            inputManager = InputManager.Instance;
            _cinemachineTargetYaw = targetTransform.transform.rotation.eulerAngles.y;
        }

        private void LateUpdate()
        {
            Rotation();
        }

        private void Rotation()
        {
            if (inputManager.Look.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                //float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
                float deltaTimeMultiplier = 1.0f;

                _cinemachineTargetYaw += inputManager.Look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += inputManager.Look.y * deltaTimeMultiplier;
            }

            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            targetTransform.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

    }
}
