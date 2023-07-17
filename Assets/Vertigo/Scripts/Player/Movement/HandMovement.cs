using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class HandMovement : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inputScrollHands;
        [SerializeField] private InputActionReference _inputHandMovement;

        [SerializeField][Range(1, 300)] private float _sensitivityRotationHand = 1.5f;
        [SerializeField][Range(1, 300)] private float _sideMovementSensitivity = 1.3f;
        [SerializeField][Range(1, 300)] private float _forwardMovementSensitivity = 2;

        private float _yHandRotation = 0f;

        private void Start()
        {
            _inputScrollHands.action.started += MovingHand;
        }

        private void MovingHand(InputAction.CallbackContext context)
        {
            _yHandRotation -= _inputScrollHands.action.ReadValue<Vector2>().y * _sensitivityRotationHand * Time.deltaTime;
            _yHandRotation = Mathf.Clamp(_yHandRotation, -70, 25f);
            transform.localRotation = Quaternion.Euler(_yHandRotation, 0, 0);
        }

        public Vector3 GetMovementDirection(bool normalized = true)
        {
            // Calculate the throw direction based on the mouse delta position
            Vector2 scrollMovement;
            Vector2 mouseMovement;
            if (normalized)
            {
                scrollMovement = Time.deltaTime * (_inputScrollHands.action.ReadValue<Vector2>().normalized);
                mouseMovement = Time.deltaTime * (_inputHandMovement.action.ReadValue<Vector2>().normalized);
            }
            else
            {
                scrollMovement = Time.deltaTime * _inputScrollHands.action.ReadValue<Vector2>();
                mouseMovement = Time.deltaTime * _inputHandMovement.action.ReadValue<Vector2>();
            }
            Vector2 combinedMovement = mouseMovement + scrollMovement;
            Vector3 movementDirection = Vector3.zero;

            if (combinedMovement.y != 0)
            {
                movementDirection += (Vector3.forward * combinedMovement.y) * _forwardMovementSensitivity;
            }
            if (combinedMovement.x != 0f)
            {
                movementDirection += (Vector3.right * combinedMovement.x) * _sideMovementSensitivity;
            }
            
            return movementDirection;
        }
    }
}