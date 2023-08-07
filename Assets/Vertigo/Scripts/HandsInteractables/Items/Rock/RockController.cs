namespace Vertigo.Player.Interactables
{
    public class RockController : ItemController<RockView>
    {
        private RockModel _rockModel;
        public RockController(RockView view, RockModel RockModel ) : base(view)
        {
            _rockModel = RockModel;
            _usagesLeft = _rockModel.Usages;
        }

        public override void Release()
        {
            base.Release();
            _usagesLeft = _rockModel.Usages;
        }
    }
}