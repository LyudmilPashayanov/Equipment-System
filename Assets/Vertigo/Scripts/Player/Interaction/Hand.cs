using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    private const float RAY_DISTANCE = 3.9f;
    private const float PICK_UP_DURATION = 0.5f;

    [SerializeField] private EquipmentManager _equipmentManager;

    [SerializeField] private InputActionReference _inputUse;
    [SerializeField] private InputActionReference _inputEquip;
    [SerializeField] private InputActionReference _inputHandMovement;
    [SerializeField] private InputActionReference _inputToggleMode;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _itemLayerMask;
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

    public Item GetItem()
    {
        return _equippedItem;
    }

    public EquipmentManager GetEquipmentManager() 
    {
        return _equipmentManager;
    }

    private void Start()
    {
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
        _lineRenderer.SetPosition(0, _ray.origin);
        if (Physics.Raycast(_ray, out HandsRayHit, RAY_DISTANCE, _itemLayerMask))
        {
            _rayHitObject = HandsRayHit.collider.gameObject;
            _lineRenderer.SetPosition(1, HandsRayHit.point);
            _lookingAtItem = true;
            // TODO: Maybe add an indicator what you are hitting
        }
        else
        {
            _lookingAtItem = false;
            _rayHitObject = null;
            _lineRenderer.SetPosition(1, _ray.origin + _ray.direction * RAY_DISTANCE);
        }
        ChangeLineColor();
    }

    private void ChangeLineColor() // change colors once, not every frame 
    {
        if (_lineRenderer.enabled == false && _equippedItem == null) 
        {
            _lineRenderer.enabled = true;
        }
        else if(_lineRenderer.enabled == true && _equippedItem) 
        {
            _lineRenderer.enabled = false;
        }
        if (_lookingAtItem)
        {
            _lineRenderer.startColor = Color.green;
            _lineRenderer.endColor = Color.green;
        }
        else
        {
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
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
        }
    }

    private void EquipItem()
    {
        _equippedItem.Equip(this);
        _equippedItem.transform.SetParent(_palm.transform, true);
        _pickUpSequence = DOTween.Sequence();
        _pickUpSequence.Append(_equippedItem.transform.DOLocalMove(Vector3.zero, PICK_UP_DURATION));
        _pickUpSequence.Insert(0, _equippedItem.transform.DOLocalRotate(new Vector3(0, -90, 0), PICK_UP_DURATION));
    }

    public void UnequipCurrentItem()
    {
        if (_pickUpSequence.active)
        {
            _pickUpSequence.Kill();
        }
        _equippedItem.transform.SetParent(null);
        _equippedItem.Unequip(GetThrowDirection(), _throwForce);
        _equippedItem = null;
    }

    private Vector3 GetThrowDirection()
    {
        // Calculate the throw direction based on the mouse delta position
        Vector2 mouseMovement = _inputHandMovement.action.ReadValue<Vector2>();
        Vector3 throwDirection = Vector3.zero;
        if (mouseMovement.y > 0)
        {
            throwDirection += (Vector3.forward * mouseMovement.y) * _throwForwardSensitivity;
        }
        if (mouseMovement.x != 0f)
        {
            throwDirection += (Vector3.right * mouseMovement.x)* _throwSidesSensitivity;
        }
        
        return Quaternion.Euler(0f, transform.parent.rotation.eulerAngles.y, 0f) * throwDirection;
    }

    private void StartUse(InputAction.CallbackContext context)
    {
        if (_modifierPressed) 
        {
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
