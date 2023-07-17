using UnityEngine;

namespace Player.Interactables
{
    public class HatController : HatItem
    {
        [SerializeField] private HatView _view;
        [SerializeField] protected Collider _onHeadCollider;
        private bool _onHead;

        private void Start()
        {
            _onHeadCollider.enabled = false;
        }

        public override void StartUse()
        {
            bool hatEquipped = _handHolder.GetEquipmentManager().TryEquipHat(this);
            if (hatEquipped)
            {
                EquipOnHead();
            }
            else
            {
                _view.UnusableIndication();
            }
        }

        public override void Grab(Hand Hand)
        {
            base.Grab(Hand);
            _onHeadCollider.enabled = false;
            if (_onHead)
                _handHolder.GetEquipmentManager().UnequipHat();
        }

        private void EquipOnHead() 
        {
            _handHolder.ReleaseCurrentItem(false);
            UnsubscribeHand();
            _handHolder = null;
            _onHeadCollider.enabled = true;
            _onHead = true;
        }
    }
}
