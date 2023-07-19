using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Vertigo.Player.Movement
{
    /// <summary>
    /// This class is responsible to move the player forward by handling the Input given from the Unity Input system.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        [Header("WALKING")]
        [SerializeField] private InputActionReference _input;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _acceleration = 90;
        [SerializeField] private float _moveClamp = 13;
        [SerializeField] private float _deAcceleration = 60f;

        public Action<Vector3> OnPlayerMove;

        private Vector3 RawMovement;
        private float _currentXSpeed;
        #endregion 

        #region Functionality
        void Update()
        {
            CalculateWalk();
        }

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

        /// <summary>
        /// Moving the player only forward, as in VR I would imagine this is better.
        /// </summary>
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
        #endregion

    }
}