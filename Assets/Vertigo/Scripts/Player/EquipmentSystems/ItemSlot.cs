using UnityEngine;
using Vertigo.Player.Interactables;

public class ItemSlot : MonoBehaviour
{
    protected IUsableItem _equippedItem;
    [SerializeField] private Transform _parent;
    public IUsableItem GetEquippedItem() => _equippedItem;
    bool _available = true;
    public bool IsAvailable => _available;
    public ItemSlot()
    { }

    public void Equip(IUsableItem item)
    {
        _equippedItem = item;
        item.SetParent(_parent, true);
        _available = false;
    }

    public IUsableItem UnequipItem()
    {
        _equippedItem.SetParent(null, true);
        _available = true;
        IUsableItem itemToReturn = _equippedItem;
        _equippedItem = null;
        return itemToReturn;
    }
}
