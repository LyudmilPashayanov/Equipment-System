namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// This class contains the business and Unity logic of the Ammo-clip item as a Grabbable game object.
    /// </summary>
    public class AmmoClipController : ItemController<AmmoClipView>, ICombinableItem
    {
        #region Variables
        private AmmoClipModel _model;
        private bool _usable = true;

        public AmmoClipController(AmmoClipView view, AmmoClipModel model) : base(view)
        {
            _model = model;
        }

        #endregion
        #region Functionality

        public void TryCombineWithItemInOtherHand(ItemController otherItem)
        {
            if (_usable)
            {
                if (otherItem is GunController)
                {
                    GunController gun = (GunController)otherItem;
                    // _view.ReloadAnimation(_model.reloadTime, gun.transform, () => ReloadGun(gun));
                    _view.PlayReloadSound();
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
           // TODO: _view.Destory()? OR  UnityEngine.Object.Destroy(_view.gameObject);
        }
        #endregion
    }
}
