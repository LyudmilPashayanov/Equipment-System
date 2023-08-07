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
        public void ParentItem(Transform parent, bool worldPosStays);
        void Release();

        void ApplyThrowForce(Vector3 throwDirection, float handForce);

        public event Action OnUpdate;
    }

    public abstract class ItemView : ItemInteractable, IItemView
    {
        #region Variables
        private const float PICK_UP_DURATION = 0.5f;

        [SerializeField] protected Rigidbody _rb;
        public ItemController Controller;

        private Sequence _pickUpSequence;
        public event Action OnUpdate;
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
        public void ParentItem(Transform parent, bool worldPosStays) 
        {
            transform.SetParent(parent, worldPosStays);
        }

        public override ItemController Grab(Hand hand)
        {
            ToggleKinematic(true);
            _pickUpSequence = DOTween.Sequence();
            _pickUpSequence.Append(transform.DOLocalMove(Vector3.zero, PICK_UP_DURATION));
            _pickUpSequence.Insert(0, transform.DOLocalRotate(new Vector3(0, -90, 0), PICK_UP_DURATION));
            return Controller;
        }

        public override void Release()
        {
            if (_pickUpSequence.active)
            {
                _pickUpSequence.Kill();
            }
            ToggleKinematic(false);
        }

        protected void ToggleKinematic(bool enable)
        {
            _rb.isKinematic = enable;
        }

        public void ApplyThrowForce(Vector3 throwDirection, float force)
        {
            _rb.AddForce(throwDirection * force, ForceMode.Impulse);
        }

        #endregion
    }
}