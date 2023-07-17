using Player.Interactables;
using UnityEngine;

namespace Player
{
    public class EquipmentManager : MonoBehaviour
    {
        [SerializeField] private Hand _leftHand;
        [SerializeField] private Hand _rightHand;
        [SerializeField] private Head _head;

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
    }
}
