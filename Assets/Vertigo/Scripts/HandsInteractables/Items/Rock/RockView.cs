namespace Vertigo.Player.Interactables
{
    public class RockView : ItemView
    {
        public override void StartUse(Hand handUsingIt)
        {
            Release(); 
        }
    }
}
