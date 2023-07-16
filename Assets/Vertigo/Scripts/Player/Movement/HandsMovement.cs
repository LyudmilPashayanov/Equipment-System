using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class HandsMovement : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inputHands;

        [SerializeField] private List<Hand> _hands;

        [SerializeField][Range(1, 3)] private float _sensitivityHands = 1.5f;

        private float _yHandsRotation = 0f;

        private void Start()
        {
            _inputHands.action.started += MovingHands;
        }

        private void MovingHands(InputAction.CallbackContext context)
        {
            _yHandsRotation -= _inputHands.action.ReadValue<Vector2>().y * _sensitivityHands * Time.deltaTime;
            _yHandsRotation = Mathf.Clamp(_yHandsRotation, -80f, 80f);
            foreach (var hand in _hands)
            {
                hand.transform.localRotation = Quaternion.Euler(_yHandsRotation, 0, 0);
            }
        }
    }
}