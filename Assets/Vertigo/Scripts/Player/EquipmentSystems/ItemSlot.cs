using UnityEngine;
using Vertigo.Player.Interactables;

public class ItemSlot : MonoBehaviour
{
    protected ItemController _item;
    [SerializeField] private Transform _parent;
    public ItemController GetEquippedItem() => _item;
    
    public ItemSlot()
    { }

    public void Equip(ItemController item)
    {
        _item = item;
        item.SetParent(_parent, true);
    }

    public ItemController UnequipItem()
    {
        _item.SetParent(null, true);
        ItemController itemToReturn = _item;
        _item = null;
        return itemToReturn;
    }
}
