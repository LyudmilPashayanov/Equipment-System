using Vertigo.Player.Interactables;
using UnityEngine;

namespace Vertigo.Player
{

    /// <summary>
    /// This class represents the Head Slot of the player. It is responsible to equip a Hat to it.
    /// TODO: Maybe apply any stats, when wearing a hat.
    /// </summary>
    public class HeadItemSlot : MonoBehaviour
    {
        #region Variables
        private HatItem _equippedHat;
        #endregion

        #region Variables
        internal bool TryEquipHat(HatItem hatItem)
        {
            if (_equippedHat == null)
            {
                _equippedHat = hatItem;
               // _equippedHat.OnUnequipped += UnequipHat;
               // _equippedHat.transform.SetParent(transform, false);
                return true;
            }
            else
             //  _equippedHat.HatInUseIndication();
                return false;
        }

        public void UnequipHat()
        {
           // _equippedHat.OnUnequipped -= UnequipHat;
            _equippedHat = null;
        }
        #endregion
    }
}