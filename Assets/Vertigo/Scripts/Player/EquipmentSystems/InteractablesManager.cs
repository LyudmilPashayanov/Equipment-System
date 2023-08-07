using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible to track and share what items are equipped to the Player hands and head.
    /// </summary>
    public class InteractablesManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private ItemSlot _leftHandSlot;
        [SerializeField] private ItemSlot _rightHandSlot;
        [SerializeField] private ItemSlot _headSlot;
        #endregion

        #region Functionality
        public void EquipHandSlot(IHand handSide, ItemController item)
        {
            if(_headSlot.GetEquippedItem() == item) 
            {
                _headSlot.UnequipItem();
            }
            if(item is ItemController interactableItem) 
            {
                switch (handSide.GetHandSide())
                {
                    case HandSide.right:
                        _rightHandSlot.Equip(interactableItem);
                        break;
                    case HandSide.left:
                        _leftHandSlot.Equip(interactableItem);
                        break;
                }
            }
            /*  else if( item is StaticObjectInteractable staticObjectInteractableItem) 
            {
                staticObjectInteractableItem.grab
            }*/
           
        }

        public ItemController UnequipHandSlot(HandSide handSide)
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
        public void UseItem(IHand handUser)
        {
            HandSide handSide = handUser.GetHandSide();
            ItemController currentlyUsedItem = GetItemFromHandSlot(handSide);

            currentlyUsedItem.StartUse();
            if(currentlyUsedItem.GetUsagesLeft() == 0) 
            {
                Debug.Log("USAGES LEFT ARE 0 - RELEASING THE ITEM");
                handUser.ReleaseCurrentItem();
            }

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
                    UnequipHandSlot(handSide);
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
        public bool CheckIfHandSlotTaken(HandSide handSide)
        {
            return GetItemFromHandSlot(handSide) != null;
        }

        private ItemController GetItemFromHandSlot(HandSide handSide) 
        {
            return handSide == HandSide.right ? _rightHandSlot.GetEquippedItem() : _leftHandSlot.GetEquippedItem();
        }
        #endregion
    }
}
