using Vertigo.Player.Interactables;

public class ItemSlot
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
