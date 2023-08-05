using System;
using Vertigo.Player.Interactables;

public class HatController : ItemController<HatView>
{
    public event Action OnUnequipped;

    protected HatController(HatView view) : base(view)
    {
        _view.EnableOnHeadCollider(false);
    }

    public void TryEquipOnHead(bool successfulEquip)
    {
        if (successfulEquip)
        {
            EquipOnHead();
        }
        else
        {
            HatInUseIndication();
        }
    }

    public void HatInUseIndication()
    {
        _view.UnusableIndication();
    }


    private void EquipOnHead()
    {

        // TODO : Remove from the hand 
        /*_handHolder.ReleaseCurrentItem(false);
        UnsubscribeHand();
        _handHolder = null;*/
        _view.EnableOnHeadCollider(true);

    }
}
