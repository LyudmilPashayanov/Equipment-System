using System;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class marking a HatController as a Hat item.
    /// </summary>
    public abstract class HatItem : ItemController
    {
        [SerializeField] private ItemView _view;
        [SerializeField] protected Collider _onHeadCollider;
        
        public event Action OnUnequipped;

        private void Start()
        {
            _onHeadCollider.enabled = false;
        }

        public void TryEquipOnHead(bool successfulEquip) 
        {
            if (successfulEquip)
            {
                EquipOnHead();
            }
        }

        public void HatInUseIndication()
        {
            _view.UnusableIndication();
        }

        public override void Grab(Hand Hand)
        {
            base.Grab(Hand);
            _onHeadCollider.enabled = false;
            OnUnequipped?.Invoke();
        }

        private void EquipOnHead()
        {
            _handHolder.ReleaseCurrentItem(false);
            UnsubscribeHand();
            _handHolder = null;
            _onHeadCollider.enabled = true;
        }
    }
}
