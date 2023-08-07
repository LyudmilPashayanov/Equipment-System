using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is being controlled by the <see cref="Hand"/>. It is responsible to control <see cref="ItemInteractable"/> and <see cref="StaticInteractable"/> items, which are in the hands or/and on the head of the player.
    /// </summary>
    public class InteractablesManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private ItemSlot _leftHandItemSlot;
        [SerializeField] private ItemSlot _rightHandItemSlot;
        [SerializeField] private ItemSlot _headItemSlot;
        private IInteractable _rightHandInteractable;
        private IInteractable _leftHandInteractable;
        #endregion

        #region Functionality
        public void EquipHandSlot(IHand handOperator, IInteractable interactable)
        {
            if (interactable is ItemInteractable itemInteractable)
            {
                IUsableItem usableItem = itemInteractable.GetUsableItem();
                if (_headItemSlot.GetEquippedItem() == usableItem)
                {
                    _headItemSlot.UnequipItem();
                }
                switch (handOperator.GetHandSide())
                {
                    case HandSide.right:
                        _rightHandItemSlot.Equip(usableItem);
                        break;
                    case HandSide.left:
                        _leftHandItemSlot.Equip(usableItem);
                        break;
                }
            }
            else if (interactable is StaticInteractable staticInteractable)
            {
                staticInteractable.RegisterHand(handOperator);
                switch (handOperator.GetHandSide())
                {
                    case HandSide.right:
                        _rightHandInteractable = staticInteractable;
                        break;
                    case HandSide.left:
                        _leftHandInteractable = staticInteractable;
                        break;
                }
            }
        }

        public IUsableItem FreeHandSlot(HandSide handSide)
        {
            IUsableItem itemToReturn = null;
            switch (handSide)
            {
                case HandSide.right:
                    if (_rightHandInteractable != null)
                    {
                        _rightHandInteractable.Release();
                        _rightHandInteractable = null;
                    }
                    else if (_rightHandItemSlot.IsAvailable == false)
                    {
                        itemToReturn = _rightHandItemSlot.UnequipItem();
                        itemToReturn.Release();
                    }
                    break;
                case HandSide.left:
                    if (_leftHandInteractable != null)
                    {
                        _leftHandInteractable.Release();
                        _leftHandInteractable = null;
                    }
                    else if (_leftHandItemSlot.IsAvailable == false)
                    {
                        itemToReturn = _leftHandItemSlot.UnequipItem();
                        itemToReturn.Release();
                    }
                    break;
            }
            return itemToReturn;
        }

        /// <summary>
        /// If the hand currently using an item, uses an <see cref="ICombinableItem"/> item,
        /// this function will call the Combine method with the item in the other hand.
        /// Also will try to equip a <see cref="IHat"/> if a Hat is being used.
        /// Also trakcs how much usages an <see cref="IUsableItem"/> has and unequips it if reach 0.
        /// </summary>
        /// <param name="hand"></param>
        public void UseItem(IHand handUser)
        {
            HandSide handSide = handUser.GetHandSide();
            IUsableItem currentlyUsedItem = GetItemFromHandSlot(handSide);
            if (currentlyUsedItem != null)
            {
                currentlyUsedItem.StartUse();
                if (currentlyUsedItem is ICombinableItem)
                {
                    TryCombineItems();
                }

                if (currentlyUsedItem is IHat)
                {
                    TryEquipHat();
                }

                if (currentlyUsedItem.GetUsagesLeft() == 0)
                {
                    handUser.ReleaseCurrentInteractable();
                }

                void TryCombineItems()
                {
                    ICombinableItem currentItem = currentlyUsedItem as ICombinableItem;
                    IUsableItem otherHandItem = handSide == HandSide.right ? _leftHandItemSlot.GetEquippedItem() : _rightHandItemSlot.GetEquippedItem();
                    currentItem.TryCombineWithItemInOtherHand(otherHandItem);
                }

                void TryEquipHat()
                {
                    CowboyHatController hat = currentlyUsedItem as CowboyHatController;
                    bool canEquipOnHead = _headItemSlot.IsAvailable;
                    hat.TryEquipOnHead(canEquipOnHead);
                    if (canEquipOnHead)
                    {
                        FreeHandSlot(handSide);
                        _headItemSlot.Equip(hat);
                    }
                }
            }
        }

        public void StopUse(HandSide handSide)
        {
            IUsableItem currentlyUsedItem = GetItemFromHandSlot(handSide);
            if (currentlyUsedItem != null)
            {
                currentlyUsedItem.StopUse(); 
            }
        }

        public void ToggleItem(HandSide handSide)
        {
            IUsableItem currentlyUsedItem = GetItemFromHandSlot(handSide);
            if (currentlyUsedItem != null)
            {
                currentlyUsedItem.ToggleItem();
            }
        }

        public bool CheckIfHandSlotAvailable(HandSide handSide)
        {
            if(handSide == HandSide.right) 
            {
                return _rightHandInteractable == null && _rightHandItemSlot.IsAvailable;
            }
            else if (handSide == HandSide.left) 
            {
                return _leftHandInteractable == null && _leftHandItemSlot.IsAvailable;

            }
            return false;
        }

        private IUsableItem GetItemFromHandSlot(HandSide handSide)
        {
            return handSide == HandSide.right ? _rightHandItemSlot.GetEquippedItem() : _leftHandItemSlot.GetEquippedItem();
        }
        #endregion
    }
}
