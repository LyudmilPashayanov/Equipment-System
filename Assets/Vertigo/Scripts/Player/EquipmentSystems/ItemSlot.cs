using UnityEngine;
using Vertigo.Player.Interactables;

public abstract class ItemSlot : MonoBehaviour
{
    protected ItemController _item;
    public ItemController GetEquippedItem() => _item;

    protected void EquipItem(ItemController item)
    {
        _item = item;
    }

    protected void UnequipItem()
    {
        _item = null;
    }
}
