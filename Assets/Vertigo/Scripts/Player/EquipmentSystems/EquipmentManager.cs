using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    public class EquipmentManager : MonoBehaviour
    {
        [SerializeField] private Hand _leftHand;
        [SerializeField] private Hand _rightHand;
        [SerializeField] private Head _head;

        private void Start()
        {
            _leftHand.OnItemUse += OnItemUsed;
            _rightHand.OnItemUse += OnItemUsed;
        }

        public Grabable GetOtherHandItem(Hand _currentHand)
        {
            return (_currentHand == _leftHand ? _rightHand : _leftHand).GetItem();
        }

        public bool TryEquipHat(HatItem hatItem)
        {
            return _head.TryEquipHat(hatItem);
        }

        internal void UnequipHat()
        {
            _head.UnequipHat();
        }

        public void OnItemUsed(Hand hand)
        {
            Grabable usedItem = hand.GetItem();
            if (usedItem is ICombinableItem)
            {
                var otherGrabable = GetOtherHandItem(hand);
                Grabable item1 = hand.GetItem();
                ICombinableItem item = item1 as ICombinableItem;
                item.TryCombineWithItemInOtherHand(otherGrabable);
            }
            else if (usedItem is HatItem)
            {
                HatItem hat = usedItem as HatItem;
                hat.TryEquipOnHead(TryEquipHat(hat));
            }
        }
    }
}
