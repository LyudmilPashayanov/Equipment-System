using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Player.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private InputActionReference _input;

        [SerializeField] private Transform _cameraPivot;
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

        private void ApplyCameraMovement()
        {
            _mouseX = _input.action.ReadValue<Vector2>().x * _sensitivityX * Time.deltaTime;
            _mouseY = _input.action.ReadValue<Vector2>().y * _sensitivityY * Time.deltaTime;
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);            
            _cameraPivot.transform.localRotation= Quaternion.Euler(_xRotation,0,0);
            _player.Rotate(Vector3.up * _mouseX);
        }
    }
}
