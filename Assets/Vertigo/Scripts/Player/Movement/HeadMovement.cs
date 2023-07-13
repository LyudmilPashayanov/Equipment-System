using UnityEngine;
using UnityEngine.InputSystem;

public class HeadMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;

    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _player;
    
    [SerializeField][Range(0, 200)] private float _sensitivityX = 2;
    [SerializeField][Range(0, 150)] private float _sensitivityY = 2;

    private Quaternion _cameraGoToPivotRotation;
    
    private bool _unlockedViewPlayer;
    private float _MouseRotationDifferenceX;
    private float _MouseRotationDifferenceY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        CalculateMouseMovement();
        ApplyCameraMovement();
    }


    private void CalculateMouseMovement()
    {
        if (_unlockedViewPlayer)
        {
             _MouseRotationDifferenceX += _input.action.ReadValue<Vector2>().x * _sensitivityX* Time.deltaTime;
             _MouseRotationDifferenceY -= _input.action.ReadValue<Vector2>().y * _sensitivityY * Time.deltaTime;
            _MouseRotationDifferenceY = Mathf.Clamp(_MouseRotationDifferenceY, -80f, 80f);
            _cameraGoToPivotRotation = Quaternion.Euler(_MouseRotationDifferenceY, _MouseRotationDifferenceX, 0);
        }
    }

    private void ApplyCameraMovement()
    {
        if (_unlockedViewPlayer)
        {
            _cameraPivot.rotation = Quaternion.Slerp(_cameraPivot.rotation,_cameraGoToPivotRotation, 10 * Time.deltaTime);
            _player.forward = new Vector3(_cameraPivot.forward.x, 0, _cameraPivot.forward.z);
        }
    }

    private void GatherInput()
    {
        if (_input.action.ReadValue<Vector2>() == Vector2.zero)
        {
            _unlockedViewPlayer = false;
            return;
        }

        _unlockedViewPlayer = true;
    }
}
