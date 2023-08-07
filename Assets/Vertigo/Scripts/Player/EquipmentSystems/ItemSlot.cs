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
        item.SetParent(_parent);
    }

    public ItemController UnequipItem()
    {
        _item.SetParent(null);
        ItemController itemToReturn = _item;
        _item = null;
        return itemToReturn;
    }
}
