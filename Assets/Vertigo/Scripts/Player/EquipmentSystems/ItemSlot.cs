using UnityEngine;
using Vertigo.Player.Interactables;

public class ItemSlot : MonoBehaviour // reference it to the head/R hand/L hand
{
    protected ItemController _item;
    public ItemController GetEquippedItem() => _item;
    
    public ItemSlot()
    { }

    public void EquipItem(ItemController item)
    {
        _item = item;
    }

    public void UnequipItem()
    {
        _item = null;
    }
}
