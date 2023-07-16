using Player.Items;
using UnityEngine;

namespace Player
{
    public class Head : MonoBehaviour
    {
        private GameObject _equippedHat;

        internal bool TryEquipHat(HatItem hatItem)
        {
            if (_equippedHat == null)
            {
                _equippedHat = hatItem.gameObject;
                _equippedHat.transform.SetParent(transform, false);
                return true;
            }
            else
                return false;
        }

        public void UnequipHat()
        {
            _equippedHat = null;
        }
    }
}