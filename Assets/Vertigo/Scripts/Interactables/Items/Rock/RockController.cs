namespace Player.Interactables
{
    public class RockController : Item
    {
        public override void StartUse()
        {
            _handHolder.ReleaseCurrentItem();
        }
    }
}