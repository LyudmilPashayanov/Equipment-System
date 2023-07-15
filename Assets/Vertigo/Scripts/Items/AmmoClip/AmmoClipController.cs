using UnityEngine;

public class AmmoClipController : Item
{
    [SerializeField] private AmmoClipView _view;
    [SerializeField] private AmmoClipModel _model;
    private bool _usable = true;

    public override void StartUse()
    {
        if (_usable)
        {
            Item otherHandItem = _handHoldingIt.GetEquipmentManager().GetOtherHandItem(_handHoldingIt);
            if (otherHandItem is GunController)
            {
                GunController gun = (GunController)otherHandItem;
                _view.ReloadAnimation(_model.reloadTime, gun.transform, () => ReloadGun(gun));
                _usable = false;
            }
            else
            {
                _view.UnusableIndication();
            }
        }
    }

    private void ReloadGun(GunController gun) 
    {
        gun.ReloadBullets(_model.ammoCount);
        Destroy(gameObject);
    }
}
