using UnityEngine;
using Vertigo.Player.Interactables;

public class ItemSlot : MonoBehaviour
{
    protected IUsableItem _item;
    [SerializeField] private Transform _parent;
    public IUsableItem GetEquippedItem() => _item;
    
    public ItemSlot()
    { }

    public void Equip(IUsableItem item)
    {
        _item = item;
        item.SetParent(_parent, true);
    }

    public IUsableItem UnequipItem()
    {
        _item.SetParent(null, true);
        IUsableItem itemToReturn = _item;
        _item = null;
        return itemToReturn;
    }
}
