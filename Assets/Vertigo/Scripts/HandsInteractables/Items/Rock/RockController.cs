namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// This class contains the business logic of the Rock item.
    /// </summary>
    public class RockController : ItemController<RockView>
    {
        private RockModel _rockModel;
        public RockController(RockView view, RockModel RockModel ) : base(view)
        {
            _rockModel = RockModel;
            _usagesLeft = _rockModel.usages;
        }

        public override void Release()
        {
            base.Release();
            _usagesLeft = _rockModel.usages;
        }
    }
}