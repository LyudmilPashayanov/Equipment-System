namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// This class marks an Interactable Item, as one which can be combined with another Interactable Item which is in the other hand of the Player.
    /// </summary>
    public interface ICombinableItem
    {
        public void TryCombineWithItemInOtherHand(IUsableItem otherItem);
    }
}
