using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

public class Hand : MonoBehaviour
{
    private const float RAY_DISTANCE = 3.9f;
    private const float PICK_UP_DURATION = 0.5f;

    [SerializeField] private InputActionReference _inputUse;
    [SerializeField] private InputActionReference _inputEquip;
    [SerializeField] private InputActionReference _inputHandMovement;
    [SerializeField] private InputActionReference _inputToggleMode;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask targetLayerMask; // Item
    [SerializeField] private Transform _palm;
    
    [SerializeField] private float _throwSidesSensitivity = 1.3f;
    [SerializeField] private float _throwForce = 5f;
    [SerializeField] private float _throwForwardSensitivity = 2;

    private Ray _ray;
    private RaycastHit HandsRayHit;

    bool _lookingAtItem;
    bool _modifierPressed = false;

    private Item _equippedItem;
    private Sequence _pickUpSequence;
    private GameObject _rayHitObject;

    public event Action<Item> OnItemEquipped; // EquipmentManager will listen to this
    public event Action<Item> OnItemUnequipped; // EquipmentManager will listen to this
    private void Start()
    {
        // TODO: Maybe move this to the actual object so that code looks more clean.
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;

        _inputEquip.action.started += PerformEquip;
        _inputToggleMode.action.started += ToggleItemMode;
        _inputUse.action.started += StartUse;
        _inputUse.action.canceled += StopUse;
    }

    private void Update()
    {
        RaycastHands();
    }

    private void RaycastHands()
    {
        _ray = new Ray(transform.position, transform.forward);
        lineRenderer.SetPosition(0, _ray.origin);
        if (Physics.Raycast(_ray, out HandsRayHit, RAY_DISTANCE, targetLayerMask))
        {
            _rayHitObject = HandsRayHit.collider.gameObject;
            lineRenderer.SetPosition(1, HandsRayHit.point);
            _lookingAtItem = true;
            // TODO: Maybe add an indicator what you are hitting
        }
        else
        {
            _lookingAtItem = false;
            _rayHitObject = null;
            lineRenderer.SetPosition(1, _ray.origin + _ray.direction * RAY_DISTANCE);
        }
        ChangeLineColor();
    }

    private void ChangeLineColor() // change colors once, not every frame 
    {
        if (_lookingAtItem)
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
        else
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
    }

    private void PerformEquip(InputAction.CallbackContext context)
    {
        _modifierPressed = true;
        if (_equippedItem != null)
        {
            UnequipCurrentItem();
        }
        else
        {
            if (_rayHitObject != null)
            {
                _equippedItem = _rayHitObject.GetComponent<Item>();
                EquipItem();
            }
            else
            {
                Debug.Log("nothing is selected ");
            }
        }
    }

    private void EquipItem()
    {
        _equippedItem.Equip();
        _equippedItem.transform.SetParent(_palm.transform, true);
        _pickUpSequence = DOTween.Sequence();
        _pickUpSequence.Append(_equippedItem.transform.DOLocalMove(Vector3.zero, PICK_UP_DURATION));
        _pickUpSequence.Insert(0, _equippedItem.transform.DOLocalRotate(new Vector3(0, -90, 0), PICK_UP_DURATION));
        OnItemEquipped?.Invoke(_equippedItem);
    }

    private void UnequipCurrentItem()
    {
        if (_pickUpSequence.active)
        {
            _pickUpSequence.Kill();
        }
        _equippedItem.transform.SetParent(null);
        _equippedItem.Unequip(GetThrowDirection(), _throwForce);
        OnItemUnequipped?.Invoke(_equippedItem);
        _equippedItem = null;
    }

    private Vector3 GetThrowDirection()
    {
        // Calculate the throw direction based on the mouse delta position
        Vector2 mouseMovement = _inputHandMovement.action.ReadValue<Vector2>();
        Vector3 throwDirection = Vector3.zero;
        if (mouseMovement.y > 0)
        {
            throwDirection += Vector3.forward * _throwForwardSensitivity;
        }
        if (mouseMovement.x > 0f)
        {
            throwDirection += Vector3.right * _throwSidesSensitivity;
        }
        else if (mouseMovement.x < 0)
        {
            throwDirection += Vector3.left * _throwSidesSensitivity;
        }
        
        return Quaternion.Euler(0f, transform.parent.rotation.eulerAngles.y, 0f) * throwDirection;
    }

    private void StartUse(InputAction.CallbackContext context)
    {
        if (_modifierPressed) 
        {
            Debug.Log("ignoring StartUse, because a modifier key was pressed.");
            _modifierPressed = false;
            return;       
        }
        if (_equippedItem != null)
        {
            _equippedItem.StartUse();
        }
    }

    private void StopUse(InputAction.CallbackContext context)
    {
        if (_modifierPressed)
        {
            Debug.Log("ignoring StopUse, because a modifier key was pressed.");
            _modifierPressed = false;
            return;
        }
        if (_equippedItem != null)
        {
            _equippedItem.StopUse();
        }
    }
    
    private void ToggleItemMode(InputAction.CallbackContext context)
    {
        _modifierPressed = true;
        if (_equippedItem != null)
        {
            _equippedItem.ToggleMode();
        }
    }
}
