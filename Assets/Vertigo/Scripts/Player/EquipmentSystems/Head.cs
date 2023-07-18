using Vertigo.Player.Interactables;
using UnityEngine;

namespace Vertigo.Player
{
    public class Head : MonoBehaviour
    {
        private HatItem _equippedHat;

        internal bool TryEquipHat(HatItem hatItem)
        {
            if (_equippedHat == null)
            {
                _equippedHat = hatItem;
                _equippedHat.OnUnequipped += UnequipHat;
                _equippedHat.transform.SetParent(transform, false);
                return true;
            }
            else
                _equippedHat.HatInUseIndication();
                return false;
        }

        public void UnequipHat()
        {
            _equippedHat.OnUnequipped -= UnequipHat;
            _equippedHat = null;
        }
    }
}