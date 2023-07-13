using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    private const float RAY_DISTANCE = 3.9f;
    
    [SerializeField] private InputActionReference _inputUse;
    [SerializeField] private InputActionReference _inputEquip;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask targetLayerMask; // Item
    [SerializeField] private Transform _palm;

    private Ray _ray;
    private RaycastHit HandsRayHit;

    bool _lookingAtItem;
    private Item _equippedItem;

    GameObject _rayHitObject;

    private void Start()
    {
        // TODO: Maybe move this to the actual object so that code looks more clean.

        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;

        _inputUse.action.started += PerformUse;
        _inputEquip.action.started += PerformEquip;

    }

    private void PerformEquip(InputAction.CallbackContext context)
    {
        if (_equippedItem != null)
        {
            Debug.Log("should UnEquip the item and drop it on the floor");
            _equippedItem.UnequipItem();
            _equippedItem = null;
        }
        else
        {
            if (_rayHitObject != null)
            {
                Debug.Log("Raycast hit: " + _rayHitObject.name);
                _equippedItem = _rayHitObject.GetComponent<Item>();
                _equippedItem.EquipItem(_palm);
            }
            else
            {
                Debug.Log("nothing is selected ");
            }
        }

    }

    private void PerformUse(InputAction.CallbackContext context)
    {
        
       
    }

    private void Update()
    {
        RaycastHands();
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
            // TODO: When pointing to a interactable Item, highlight the line
        }
        else
        {
            _lookingAtItem = false;
            lineRenderer.SetPosition(1, _ray.origin + _ray.direction * RAY_DISTANCE);
        }
        ChangeLineColor();
    }
}
