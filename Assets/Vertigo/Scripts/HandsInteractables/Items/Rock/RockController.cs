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
        public override Type GetItemType()
        {
            throw new NotImplementedException();
        }

        public override bool IsCompatible(Type otherItemType)
        {
            throw new NotImplementedException();
        }
    }
}