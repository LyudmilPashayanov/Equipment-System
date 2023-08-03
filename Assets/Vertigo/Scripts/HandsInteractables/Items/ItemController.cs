using DG.Tweening;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all item controllers in the game. 
    /// </summary>
    public abstract class ItemController : Grabbable
    {
        #region Variables
        private const float PICK_UP_DURATION = 0.5f;

        [SerializeField] protected Rigidbody _rb;
        protected bool _itemEquipped;
        protected Hand _handHolder; // Should be ItemSlot
        private Sequence _pickUpSequence;

        public virtual void StartUse(Hand handUsingIt) { }
        public virtual void StopUse() { }
        public virtual void ToggleMode() { }
        #endregion
        
        #region Functionality
        protected void SubscribeHand() 
        {
            _handHolder.Subscribe(StartUse, StopUse, ToggleMode);
        }
        
        protected void UnsubscribeHand() 
        {
            _handHolder.Unsubscribe(StartUse, StopUse, ToggleMode);
        }

        public override void Grab(Hand Hand)
        {
            _handHolder = Hand;
            SubscribeHand();
            _itemEquipped = true;
            ToggleKinematic(true);

            transform.SetParent(Hand.GetPalm(), true);
            _pickUpSequence = DOTween.Sequence();
            _pickUpSequence.Append(transform.DOLocalMove(Vector3.zero, PICK_UP_DURATION));
            _pickUpSequence.Insert(0, transform.DOLocalRotate(new Vector3(0, -90, 0), PICK_UP_DURATION));
        }

        public override void Release()
        {
            if (_pickUpSequence.active)
            {
                _pickUpSequence.Kill();
            }
            UnsubscribeHand();
            transform.SetParent(null);
            ToggleKinematic(false);
            ApplyThrowForce(_handHolder.GetMovementDirection(), _handHolder.GetHandStrength()); // TODO: Get this through an event, not through a direct reference.
            _itemEquipped = false;
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