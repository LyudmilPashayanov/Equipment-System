using Player.Interactables;
using UnityEngine;

namespace Player
{
    public class Head : MonoBehaviour
    {
        private HatItem _equippedHat;

        internal bool TryEquipHat(HatItem hatItem)
        {
            if (_equippedHat == null)
            {
                _equippedHat = hatItem;
                _equippedHat.transform.SetParent(transform, false);
                return true;
            }
            else
                _equippedHat.HatInUseIndication();
                return false;
        }

        public void UnequipHat()
        {
            _equippedHat = null;
        }
    }
}