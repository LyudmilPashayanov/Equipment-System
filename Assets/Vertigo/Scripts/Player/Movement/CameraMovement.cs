using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Vertigo.Player.Movement
{
    /// <summary>
    /// This class is responsible for rotating the camera and player depending on the Input given from the Unity Input System.
    /// </summary>
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inputCamera;

        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _player;

        [SerializeField][Range(1, 3)] private float _sensitivityX = 1.5f;
        [SerializeField][Range(1, 3)] private float _sensitivityY = 1.5f;
        
        private float _xRotation = 0f;
        private float _mouseX;
        private float _mouseY;

        private void FixedUpdate()
        {
            ApplyCameraMovement();
        }

        /// <summary>
        /// Rotates the camera up and down and the player transform around it's Y axis.
        /// </summary>
        private void ApplyCameraMovement()
        {
            _mouseX = _inputCamera.action.ReadValue<Vector2>().x * _sensitivityX * Time.deltaTime;
            _mouseY = _inputCamera.action.ReadValue<Vector2>().y * _sensitivityY * Time.deltaTime;
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);            
            _camera.transform.localRotation= Quaternion.Euler(_xRotation,0,0);
            _player.Rotate(Vector3.up * _mouseX);
        }
    }
}
