using System;
using Vertigo.Player.Interactables;

public class HatController : ItemController<HatView>
{
    protected HatController(HatView view) : base(view)
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
