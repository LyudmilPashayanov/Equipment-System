namespace Player.Items
{
    public class RockController : Item
    {
        public override void StartUse()
        {
            _handHolder.UnequipCurrentItem();
        }
    }
}