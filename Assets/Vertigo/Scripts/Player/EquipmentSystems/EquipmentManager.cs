using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Player
{
    /// <summary>
    /// This class is responsible to track and share what items are equipped to the Player hands and head.
    /// </summary>
    public class EquipmentManager : MonoBehaviour
    {

        #region Variables
        [SerializeField] private Hand _leftHand;
        [SerializeField] private Hand _rightHand;
        [SerializeField] private HeadItemSlot _head;

        #endregion

        #region Functionality
        private void Start()
        {
            _leftHand.OnItemUse += OnItemUsed;
            _rightHand.OnItemUse += OnItemUsed;
        }

        public Grabbable GetOtherHandItem(Hand _currentHand)
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
