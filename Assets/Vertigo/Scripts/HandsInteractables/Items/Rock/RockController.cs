namespace Vertigo.Player.Interactables
{
    public class RockController : ItemController<RockView>
    {
        public RockController(RockView view) : base(view)
        {
        }
        protected override void UseItem()
        {
            // TODO: DropItem()
            _view.Release(); // probably wouldn't work as the hand is not iniating the action
        }
    }
}