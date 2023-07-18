using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Vertigo.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("WALKING")]
        [SerializeField] private InputActionReference _input;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _acceleration = 90;
        [SerializeField] private float _moveClamp = 13;
        [SerializeField] private float _deAcceleration = 60f;

        public Action<Vector3> OnPlayerMove;

        private Vector3 RawMovement;
        private float _currentXSpeed;

        void Update()
        {
            CalculateWalk();
        }

        //TODO : Explain why you use the FixedUpdate here.
        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            RawMovement = new Vector3(0, 0, _currentXSpeed);
            var move = RawMovement * Time.deltaTime;
            _rigidbody.velocity = transform.TransformVector(move);
            OnPlayerMove?.Invoke(move);
        }

        private void CalculateWalk()
        {
            if (_input.action.inProgress)
            {
                // Set horizontal move speed
                _currentXSpeed += _acceleration * Time.deltaTime;
                _currentXSpeed = Mathf.Clamp(_currentXSpeed, -_moveClamp, _moveClamp);
            }
            else
            {
                // No input. Let's slow the character down
                _currentXSpeed = Mathf.MoveTowards(_currentXSpeed, 0, _deAcceleration * Time.deltaTime);
            }
        }
    }
}