using System;
using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible to track and share what items are equipped to the Player hands and head.
    /// </summary>
    public class EquipmentManager
    {
        #region Variables
        [SerializeField] private ItemSlot _leftHand;
        [SerializeField] private ItemSlot _rightHand;
        [SerializeField] private ItemSlot _head;

        public EquipmentManager(Hand leftHand, Hand rightHand, ItemSlot head)
        {
            _leftHand = leftHand;
            _rightHand = rightHand;
            leftHand.OnItemUse += OnItemUsed;
            rightHand.OnItemUse += OnItemUsed;
            _head = head;
        }
        #endregion

        #region Functionality
        public ItemController GetOtherHandItem(Hand _currentHand)
        {
            return (_currentHand.GetEquippedItem() == _leftHand.GetEquippedItem() ? _rightHand.GetEquippedItem() : _leftHand.GetEquippedItem());
        }

        /*
        internal void UnequipHat()
        {
            _head.UnequipHat();
        }*/
        #endregion

        #region Event Handlers
        /// <summary>
        /// If the hand currently using an item, uses an ICombinable item,
        /// this function will call the Combine method with the item in the other hand.
        /// Also will try to equip a Hat if an Hat item is being used.
        /// </summary>
        /// <param name="hand"></param>
        public void OnItemUsed(Hand hand)
        {
            ItemController currentlyUsedItem = hand.GetEquippedItem();
            if (currentlyUsedItem is ICombinableItem)
            {
                ItemController otherItem = GetOtherHandItem(hand);
                ICombinableItem item = currentlyUsedItem as ICombinableItem;
                item.TryCombineWithItemInOtherHand(otherItem);
            }
            else if (currentlyUsedItem is CowboyHatController)
            {
                CowboyHatController hat = currentlyUsedItem as CowboyHatController;
                hat.TryEquipOnHead(_head.(hat));
            }
        }
        #endregion

    }
}
