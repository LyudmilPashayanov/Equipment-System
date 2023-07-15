public class RockController : Item
{
    public override void StartUse()
    {
        _handHoldingIt.UnequipCurrentItem();
    }
}
