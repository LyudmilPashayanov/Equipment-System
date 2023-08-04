using Vertigo.Player.Interactables;

namespace Vertigo.Player
{

    /// <summary>
    /// This class represents the Head Slot of the player. It is responsible to equip a Hat to it.
    /// TODO: Maybe apply any stats, when wearing a hat.
    /// </summary>
    public class Head : ItemSlot
    {
        #region Variables
        internal bool TryEquipHat(ItemController hatItem)
        {
            if (GetEquippedItem() == null)
            {
                EquipItem(hatItem);
                _equippedHat.OnUnequipped += UnequipHat;
                _equippedHat.transform.SetParent(transform, false);
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