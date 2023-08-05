using System;

namespace Vertigo.Player.Interactables
{
    public class RockController : ItemController<RockView>
    {
        public RockController(RockView view) : base(view)
        {
        }

        public override void StartUse(Hand handUsingIt)
        {
            _view.Release();
        }
    }
}