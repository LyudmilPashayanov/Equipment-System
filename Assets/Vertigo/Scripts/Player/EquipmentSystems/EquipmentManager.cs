using System;
using System.Threading;
using UnityEngine;
using Vertigo.Player.Interactables;
using static UnityEditor.Progress;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible to track and share what items are equipped to the Player hands and head.
    /// </summary>
    public class EquipmentManager : MonoBehaviour
    {
        #region Variables
        private ItemSlot _leftHandSlot;
        private ItemSlot _rightHandSlot;
        private ItemSlot _headSlot;
        #endregion

        #region Functionality
        public void EquipHandItem(HandSide handSide, ItemController item)
        {
            switch (handSide)
            {
                case HandSide.right:
                    _rightHandSlot.Equip(item);
                    break;
                case HandSide.left:
                    _leftHandSlot.Equip(item);
                    break;
            }
        }

        public ItemController UnequipSlot(HandSide handSide)
        {
            ItemController itemToReturn = null;
            switch (handSide)
            {
                case HandSide.right:
                    itemToReturn = _rightHandSlot.UnequipItem();
                    break;
                case HandSide.left:
                    itemToReturn = _leftHandSlot.UnequipItem();
                    break;
            }
            itemToReturn.Release();
            return itemToReturn;
        }

        /// <summary>
        /// If the hand currently using an item, uses an ICombinable item,
        /// this function will call the Combine method with the item in the other hand.
        /// Also will try to equip a Hat if an Hat item is being used.
        /// </summary>
        /// <param name="hand"></param>
        public void UseItem(HandSide handSide)
        {
            ItemController currentlyUsedItem = GetItemFromHandSlot(handSide);
            currentlyUsedItem.StartUse();

            if (currentlyUsedItem is ICombinableItem)
            {
                TryCombineItems();
            }
            else if (currentlyUsedItem is HatController)
            {
                TryEquipHat();
            }

            void TryCombineItems()
            {
                ICombinableItem currentItem = currentlyUsedItem as ICombinableItem;
                ItemController otherHandItem = handSide == HandSide.right ? _leftHandSlot.GetEquippedItem() : _rightHandSlot.GetEquippedItem();
                currentItem.TryCombineWithItemInOtherHand(otherHandItem);
            }

            void TryEquipHat()
            {
                HatController hat = currentlyUsedItem as HatController;
                bool canEquipOnHead = _headSlot.GetEquippedItem() == null;
                hat.TryEquipOnHead(canEquipOnHead);
                if (canEquipOnHead)
                {
                    UnequipSlot(handSide);
                    _headSlot.Equip(hat);
                }
            }
        }

        public void StopUse(HandSide handSide)
        {
            ItemController currentlyUsedItem = GetItemFromHandSlot(handSide);
            currentlyUsedItem.StopUse();
        }

        public void ToggleItem(HandSide handSide)
        {
            ItemController currentlyUsedItem = GetItemFromHandSlot(handSide);
            currentlyUsedItem.ToggleItem();
        }

        private ItemController GetItemFromHandSlot(HandSide handSide) 
        {
            return handSide == HandSide.right ? _rightHandSlot.GetEquippedItem() : _leftHandSlot.GetEquippedItem();
        }

        #endregion
    }
}
