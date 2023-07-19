namespace Vertigo.Player.Interactables
{
    public interface ICombinableItem
    {
        public void TryCombineWithItemInOtherHand(Grabable otherItem);
    }
}
