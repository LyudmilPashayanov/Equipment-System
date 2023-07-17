using Player.Interactables;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

namespace Player
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

        private void _leftHand_OnItemUse(ItemController obj)
        {
            throw new System.NotImplementedException();
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
            if (usedItem is ICombinableItemm)
            {
                var otherGrabable = GetOtherHandItem(hand);
                Grabable item1 = hand.GetItem();
                ICombinableItemm item = item1 as ICombinableItemm;
                item.TryCombineWithItem(otherGrabable);   
            }
            else if(usedItem is HatItem) 
            {
                HatItem hat = usedItem as HatItem;
                hat.TryEquipOnHead(TryEquipHat(hat));
            }

            // the item will use itself if it isn't requiring the equipment manager            
        }
    }
}
