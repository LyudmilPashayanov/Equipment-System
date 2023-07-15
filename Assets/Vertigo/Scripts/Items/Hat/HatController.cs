using UnityEngine;

namespace Player.Items
{
    public class HatController : Item, IHat
    {
        [SerializeField] private HatView _view;

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public override void StartUse()
        {
            bool hatEquipped = _handHolder.GetEquipmentManager().TryEquipHat(this);
            if (hatEquipped)
            {
                _handHolder.UnequipCurrentItem(false);
            }
            else
            {
                _view.UnusableIndication();
            }
        }
    }
}
