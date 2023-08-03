namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// This class marks a Grabbable Item, as one which can be combined with another Grabbable item in the other hand of the Player.
    /// </summary>
    public interface ICombinableItem
    {
        public void TryCombineWithItemInOtherHand<TView>(ItemController<ItemView> otherItem);
    }
}
