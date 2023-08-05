namespace Vertigo.Player.Interactables
{
    public class CowboyHatView : HatView
    { 
        public override void InitController()
        {
            Controller = new CowboyHatController(this);
        }
    }
}