using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Vertigo.Player.Movement;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible for grabbing items, using them and overall interact with the world in the game.
    /// </summary>
    public class Hand : MonoBehaviour
    {
        #region Variables
        private const float RAY_DISTANCE = 3.9f;

        [SerializeField] private HandMovement _handMovement;

        [SerializeField] private InputActionReference _inputUse;
        [SerializeField] private InputActionReference _inputEquip;
        [SerializeField] private InputActionReference _inputToggleMode;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private LayerMask _itemLayerMask;
        [SerializeField] private Transform _palm;

        [SerializeField] private float _releaseForce = 5f;

        private bool _lookingAtItem;
        private bool _modifierPressed = false;

        private Ray _ray;
        private RaycastHit HandsRayHit;
        private GameObject _rayHitObject;

        private ItemSlot _itemSlot;

        public event Action<Hand> OnItemUse;
        private event Action OnStopUse;
        private event Action OnToggleMode;
        #endregion 
        
        #region Getters

        public Transform GetPalm()
        {
            return _palm;
        }

        public float GetHandStrength() 
        {
            return _releaseForce;
        }
        #endregion

        #region Functionality

        public void Subscribe(Action<Hand> onUse, Action onStopUse, Action onToggle)
        {
            OnItemUse += onUse;
            OnStopUse += onStopUse;
            OnToggleMode += onToggle;
        }
        
        public void Unsubscribe(Action<Hand> onUse, Action onStopUse, Action onToggle)
        {
            OnItemUse -= onUse;
            OnStopUse -= onStopUse;
            OnToggleMode -= onToggle;
        }

        private void Start()
        {
            _inputEquip.action.started += PerformGrab;
            _inputToggleMode.action.started += ToggleItemMode;
            _inputUse.action.started += StartUse;
            _inputUse.action.canceled += StopUse;
        }

        private void Update()
        {
            RaycastHands();
            if(_itemSlot != null) 
            {
                CheckIfItemInRange();
            }
        }

        public Vector3 GetMovementDirection(bool normalized = true) 
        {
            return _handMovement.GetMovementDirection(normalized);
        }

        private void CheckIfItemInRange()
        {
            if(Vector3.Distance(_itemSlot.GetItem().transform.position, transform.position) > 8)
            {
                ReleaseCurrentItem();
            }
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

        private void ChangeLineColor() // changes colors once, not every frame 
        {
            if (_lineRenderer.enabled == false && _itemSlot == null)
            {
                _lineRenderer.enabled = true;
            }
            else if (_lineRenderer.enabled == true && _itemSlot != null)
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

        private void PerformGrab(InputAction.CallbackContext context)
        {
            _modifierPressed = true;
            if (_itemSlot.GetItem() != null)
            {
                ReleaseCurrentItem();
            }
            else
            {
                if (_rayHitObject != null)
                {
                    Grabbable itemToGrab;
                    _rayHitObject.TryGetComponent<Grabbable>(out itemToGrab);
                    if (itemToGrab)
                    {
                        _itemSlot.Equip(itemToGrab);
                    }
                    if(itemToGrab is ItemController) 
                    {
                        
                    }
                }
            }
        }

        public void ReleaseCurrentItem(bool throwItem = true)
        {
            if(throwItem) 
            {
                _itemSlot.Unequip();
            }
        }
        #endregion

        #region Event Handlers

        private void StartUse(InputAction.CallbackContext context)
        {
            if (_modifierPressed)
            {
                _modifierPressed = false;
                return;
            }
            if (_itemSlot != null)
            {
                OnItemUse?.Invoke(this);
            }
        }

        private void StopUse(InputAction.CallbackContext context)
        {
            if (_modifierPressed)
            {
                _modifierPressed = false;
                return;
            }
            if (_itemSlot != null)
            {
                OnStopUse?.Invoke();
            }
        }

        private void ToggleItemMode(InputAction.CallbackContext context)
        {
            _modifierPressed = true;
            if (_itemSlot != null)
            {
                OnToggleMode?.Invoke();
            }
        }
#endregion
    }
}
