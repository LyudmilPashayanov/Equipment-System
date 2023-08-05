using System;
using System.Threading;
using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible to track and share what items are equipped to the Player hands and head.
    /// </summary>
    public enum Slot
    {
        LHand,
        RHand,
        Head
    }
    public class EquipmentManager
    {
        #region Variables
        [SerializeField] private ItemSlot _leftHandSlot;
        [SerializeField] private ItemSlot _rightHandSlot;
        [SerializeField] private ItemSlot _headSlot;
        public event Action<ItemController> OnHatEquipped;

        public EquipmentManager(Hand leftHand, Hand rightHand, Head head)
        {
            _leftHandSlot = new ItemSlot();
            _rightHandSlot = new ItemSlot();
            _headSlot = new ItemSlot();

            OnHatEquipped += head.SetHat;

            leftHand.OnItemGrab += EquipSlot;
            leftHand.OnItemUse += OnItemUsed;
            leftHand.OnItemRelease += UnequipSlot;

            rightHand.OnItemGrab += EquipSlot;
            rightHand.OnItemUse += OnItemUsed;
            rightHand.OnItemRelease += UnequipSlot;
        }

        private void EquipSlot(Hand hand, ItemController controller)
        {
            GetSlotFromHand(hand).EquipItem(controller);
            hand.OnItemUse += controller.StartUse;
            hand.OnStopUse += controller.StopUse;
            hand.OnToggleMode += controller.ToggleItem;
            hand.OnItemRelease += controller.ReleaseItem;
        } 
        
        private void UnequipSlot(Hand hand)
        {
            ItemSlot slotToUnequip = GetSlotFromHand(hand);

            hand.OnItemUse -= slotToUnequip.GetEquippedItem().StartUse;
            hand.OnStopUse -= slotToUnequip.GetEquippedItem().StopUse;
            hand.OnToggleMode += slotToUnequip.GetEquippedItem().ToggleItem;
            hand.OnItemRelease += slotToUnequip.GetEquippedItem().ReleaseItem;
            slotToUnequip.UnequipItem();
        }

        private ItemSlot GetSlotFromHand(Hand hand) 
        {
            switch (hand.GetHandSide())
            {
                case HandSide.right:
                    return _rightHandSlot;
                case HandSide.left:
                    return _leftHandSlot;
            }
            return null;
        }
        #endregion

        #region Functionality
        /*        public ItemController GetOtherHandItem(Hand _currentHand)
                {
                    return (_currentHand.GetEquippedItem() == _leftHandSlot.GetEquippedItem() ? _rightHandSlot.GetEquippedItem() : _leftHandSlot.GetEquippedItem());
                }*/

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
            ItemController currentlyUsedItem = hand.GetHandSide() == HandSide.right ? _rightHandSlot.GetEquippedItem() : _leftHandSlot.GetEquippedItem();

            if (currentlyUsedItem is ICombinableItem)
            {
                TryCombineItems();
            }        
            else if (currentlyUsedItem is CowboyHatController)
            {
                TryEquipHat();
            }

            void TryCombineItems()
            {
                ICombinableItem currentItem = currentlyUsedItem as ICombinableItem;
                ItemController otherHandItem = hand.GetHandSide() == HandSide.right ? _leftHandSlot.GetEquippedItem() : _rightHandSlot.GetEquippedItem();
                currentItem.TryCombineWithItemInOtherHand(otherHandItem);
            }

            void TryEquipHat() 
            {
                if (_headSlot.GetEquippedItem() != null)
                    return;

                CowboyHatController hat = currentlyUsedItem as CowboyHatController;
                _headSlot.EquipItem(hat);
                OnHatEquipped?.Invoke(hat);

                hat.TryEquipOnHead(_headSlot.GetEquippedItem() == null);
            }
        }
        #endregion

    }
}
