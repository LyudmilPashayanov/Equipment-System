namespace Vertigo.Player.Interactables
{
    public class CowboyHatController : ItemController<CowboyHatView>, IHat
    {
        public CowboyHatController(CowboyHatView view) : base(view)
        {
            _view.EnableOnHeadCollider(false);
        }

        public void TryEquipOnHead(bool equipped)
        {
            if (equipped)
            {
                _view.EnableOnHeadCollider(true);
            }
            else
            {
                _view.UnusableIndication();
            }
        }
    }
}
