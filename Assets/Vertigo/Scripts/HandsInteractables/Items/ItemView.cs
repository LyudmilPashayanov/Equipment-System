using UnityEngine;
using DG.Tweening;
using System;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all Item Views in the game, handling all the visual changes of the items.
    /// </summary>


    public interface IItemView
    {
        public void InitController();
        public event Action OnUpdate;
/*        public event Action OnItemUse;
        public event Action OnItemStopUse;
        public event Action OnItemToggle;
        public event Action OnItemReleased;
    }*/

    public abstract class ItemView : Grabbable, IItemView
    {
        #region Variables
        private const float PICK_UP_DURATION = 0.5f;

        [SerializeField] protected Rigidbody _rb;
  
        private Sequence _pickUpSequence;

        protected HandInput _handHolder;

        public ItemController Controller;

        public event Action OnUpdate;
/*        public event Action OnItemUse;
        public event Action OnItemStopUse;
        public event Action OnItemToggle;
        public event Action OnItemReleased;*/
        #endregion

        #region Functionality
        public abstract void InitController();

        private void Awake()
        {
            InitController();
        }

        protected virtual void Update()
        {
            OnUpdate?.Invoke();
        }

        public override ItemController Grabbed(HandInput Hand)
        {
            _handHolder = Hand;
            ToggleKinematic(true);
            transform.SetParent(_handHolder.GetPalm(), true); // TODO: parent the item inside the hand
            _pickUpSequence = DOTween.Sequence();
            _pickUpSequence.Append(transform.DOLocalMove(Vector3.zero, PICK_UP_DURATION));
            _pickUpSequence.Insert(0, transform.DOLocalRotate(new Vector3(0, -90, 0), PICK_UP_DURATION));
            return Controller;
        }

        public override void Release() // Maybe pass in the hand which is releasing it
        {
            if (_pickUpSequence.active)
            {
                _pickUpSequence.Kill();
            }
            transform.SetParent(null);
            ToggleKinematic(false);
            ApplyThrowForce(_handHolder.GetMovementDirection(), _handHolder.GetHandStrength()); // TODO: Get this through an event, not through a direct reference.
            _handHolder = null;
        }

        protected void ToggleKinematic(bool enable)
        {
            _rb.isKinematic = enable;
        }

        protected void ApplyThrowForce(Vector3 handMovementDirection, float throwForce)
        {
            Vector3 throwDirection = Quaternion.Euler(0f, _handHolder.transform.parent.rotation.eulerAngles.y, 0f) * handMovementDirection;
            _rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }

        #endregion
    }
}