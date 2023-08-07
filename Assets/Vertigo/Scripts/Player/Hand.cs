using UnityEngine;
using UnityEngine.InputSystem;
using Vertigo.Player.Interactables;
using Vertigo.Player.Movement;

namespace Vertigo.Player
{
    public enum HandSide
    {
        right,
        left
    }

    public interface IHand
    {
        public Vector3 GetMovementDirection(bool normalized = true);
        public Vector3 GetPosition();
        public HandSide GetHandSide();
        public void ReleaseCurrentInteractable();

    }

    /// <summary>
    /// This class is responsible for grabbing items and handling input commands from the player. Then it just passes that information to the <see cref="InteractablesManager"/>.
    /// </summary>
    public class Hand : MonoBehaviour, IHand
    {
        #region Variables
        private const float RAY_DISTANCE = 3.9f;
        [SerializeField] private HandSide _handSide;
        [SerializeField] private HandMovement _handMovement;
        [SerializeField] private InteractablesManager _interactablesSystem;

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

        public Vector3 GetPosition()
        {
            return transform.position;
        }

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

        private void RaycastHands()
        {
            _ray = new Ray(transform.position, transform.forward);
            _lineRenderer.SetPosition(0, _ray.origin);
            if (Physics.Raycast(_ray, out HandsRayHit, RAY_DISTANCE, _itemLayerMask))
            {
                _rayHitObject = HandsRayHit.collider.gameObject;
                _lineRenderer.SetPosition(1, HandsRayHit.point);
                _lookingAtItem = true;
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
            return !_interactablesSystem.CheckIfHandSlotAvailable(_handSide);
        }
        #endregion

        #region Event Handlers

        private void PerformGrab(InputAction.CallbackContext context)
        {
            _modifierPressed = true;
            if (IsHandBusy())
            {
                ReleaseCurrentInteractable();
            }
            else
            {
                IInteractable _interactable;
                if (_rayHitObject != null && _rayHitObject.TryGetComponent(out _interactable))
                {
                    _interactablesSystem.EquipHandSlot(this, _interactable);
                }
            }
        }

        public void ReleaseCurrentInteractable()
        {
            IUsableItem uneqippedItem = _interactablesSystem.FreeHandSlot(_handSide);
            if (uneqippedItem != null)
            {
                ApplyThrowForceToItem(uneqippedItem);
            }

        }
        private void StartUse(InputAction.CallbackContext context)
        {
            if (_modifierPressed)
            {
                _modifierPressed = false;
                return;
            }                
            _interactablesSystem.UseItem(this);          
        }

        private void StopUse(InputAction.CallbackContext context)
        {
            if (_modifierPressed)
            {
                _modifierPressed = false;
                return;
            }                
            _interactablesSystem.StopUse(_handSide);         
        }

        private void ToggleItemMode(InputAction.CallbackContext context)
        {
            _modifierPressed = true;                
            _interactablesSystem.ToggleItem(_handSide);
        }
        #endregion
    }
}
