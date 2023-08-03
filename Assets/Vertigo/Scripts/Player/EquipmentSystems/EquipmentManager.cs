using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible to track and share what items are equipped to the Player hands and head.
    /// </summary>
    public class ItemSlot 
    {
        private Grabbable _item;
        
        public Grabbable GetItem()
        {
            return _item;
        }

        public void Equip(Grabbable item)
        {
            _item = item;
        }

        public void Unequip() 
        {
            _item = null;
        }
    }

    public class EquipmentManager
    {
        #region Variables
        [SerializeField] private Hand _leftHand;  // Should be ItemSlot
        [SerializeField] private Hand _rightHand; // Should be ItemSlot
        [SerializeField] private Head _head;      // Should be ItemSlot

        #endregion

        #region Functionality
        private void Init() // TODO: we need to call this and inject the references of the ItemSlots
        {
            _leftHand.OnItemUse += OnItemUsed;
            _rightHand.OnItemUse += OnItemUsed;
        }

        public Grabbable GetOtherHandItem(Hand _currentHand)
        {
            return (_currentHand == _leftHand ? _rightHand : _leftHand).GetItem(); // TODO: you get this directly from the item slot
        }

        public bool TryEquipHat(HatItem hatItem)
        {
            return _head.TryEquipHat(hatItem);
        }

        internal void UnequipHat()
        {
            _head.UnequipHat();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// If the hand currently using an item, uses an ICombinable item,
        /// this function will call the Combine method with the item in the other hand.
        /// Also will try to equip a Hat if an Hat item is being used.
        /// </summary>
        /// <param name="hand">The hand which is using the item.</param>
        public void OnItemUsed(Hand hand)
        {
            Grabbable usedItem = hand.GetItem();
            if (usedItem is ICombinableItem)
            {
                var otherGrabable = GetOtherHandItem(hand);
                Grabbable item1 = hand.GetItem();
                ICombinableItem item = item1 as ICombinableItem;
                item.TryCombineWithItemInOtherHand(otherGrabable);
            }
            else if (usedItem is HatItem)
            {
                HatItem hat = usedItem as HatItem;
                hat.TryEquipOnHead(TryEquipHat(hat));
            }
        }
        #endregion

    }
}
