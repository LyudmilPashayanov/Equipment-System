using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vertigo.Player.Interactables;
using Vertigo.Player.Movement;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible for grabbing items, using them and overall interact with the world in the game.
    /// </summary>
    public enum HandSide 
    {
        right,
        left
    }

    public interface IHand 
    {
        public Vector3 GetMovementDirection(bool normalized = true);
        public HandSide GetHandSide();
        public void ReleaseCurrentItem();

    }
    public class Hand : MonoBehaviour, IHand
    {
        #region Variables
        private const float RAY_DISTANCE = 3.9f;
        [SerializeField] private HandSide _handSide;
        [SerializeField] private HandMovement _handMovement;
        [SerializeField] private InteractablesManager _equipmentManager;

        [SerializeField] private InputActionReference _inputUse;
        [SerializeField] private InputActionReference _inputEquip;
        [SerializeField] private InputActionReference _inputToggleMode;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private LayerMask _itemLayerMask;

        [SerializeField] private float _handForce = 5f;

        private bool _lookingAtItem;
        private bool _modifierPressed = false;

        private Ray _ray;
        private RaycastHit HandsRayHit;
        private GameObject _rayHitObject;

        #endregion

        #region Getters

        public HandSide GetHandSide() 
        {
            return _handSide;
        }
        public Vector3 GetMovementDirection(bool normalized = true)
        {
            return _handMovement.GetMovementDirection(normalized);
        }
        #endregion

        #region Functionality

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
        }

        private void CheckHandRange()
        {
            //TODO: Fix this or find another solution
            
            /*if (Vector3.Distance(_itemSlot.transform.position, transform.position) > 8)
           // {
                ReleaseCurrentItem();
          //  }*/
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
            if (_lineRenderer.enabled == false && IsHandBusy() == false)
            {
                _lineRenderer.enabled = true;
            }
            else if (_lineRenderer.enabled == true && IsHandBusy() == true)
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
        
        protected void ApplyThrowForceToItem(IUsableItem item)
        {
            Vector3 throwDirection = Quaternion.Euler(0f, transform.parent.rotation.eulerAngles.y, 0f) * GetMovementDirection();
            item.AddThrowForce(throwDirection, _handForce);
        }

        private bool IsHandBusy()
        {
            return _equipmentManager.CheckIfHandSlotTaken(_handSide);            
        }
        #endregion

        #region Event Handlers
                    
        private void PerformGrab(InputAction.CallbackContext context)
        {
            _modifierPressed = true;
            if (IsHandBusy())
            {
                ReleaseCurrentItem();
            }
            else
            {
                IInteractable _interactable;
                if (_rayHitObject != null && _rayHitObject.TryGetComponent(out _interactable))
                {
                    _equipmentManager.EquipHandSlot(this, _interactable.GrabItem());
                    /*if (_interactable is ItemInteractable item)
                    {
                        ItemController itemController = item.Grab(this);
                        _equipmentManager.EquipHandItem(_handSide, itemController);
                        _isEquipped = true;
                    }
                    else if (_interactable is StaticObjectInteractable staticObject)
                    {
                        staticObject.Grab(this);
                        StopInteracting += staticObject.Release;
                        _isInteracting = true;
                    }*/
                }
            }
        }

        public void ReleaseCurrentItem()
        {
            if (IsHandBusy())
            {
                IUsableItem uneqippedItem = _equipmentManager.UnequipHandSlot(_handSide);
                ApplyThrowForceToItem(uneqippedItem);
            }
/*            else if (_isInteracting)
            {
                StopInteracting?.Invoke();
                StopInteracting = null;
                _isInteracting = false;
            }*/

        }
        private void StartUse(InputAction.CallbackContext context)
        {
            if (_modifierPressed)
            {
                _modifierPressed = false;
                return;
            }
            if (IsHandBusy())
            {
                _equipmentManager.UseItem(this);
            }
        }

        private void StopUse(InputAction.CallbackContext context)
        {
            if (_modifierPressed)
            {
                _modifierPressed = false;
                return;
            }
            if (IsHandBusy())
            {
                _equipmentManager.StopUse(_handSide);
            }
        }

        private void ToggleItemMode(InputAction.CallbackContext context)
        {
            _modifierPressed = true;
            if (IsHandBusy())
            {
                _equipmentManager.ToggleItem(_handSide);
            }
        }
        #endregion
    }
}
