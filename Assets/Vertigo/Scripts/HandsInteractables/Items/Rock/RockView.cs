namespace Vertigo.Player.Interactables
{
    public class RockView : ItemView
    {
        public override void InitController()
        {
            Controller = new RockController(this);
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
