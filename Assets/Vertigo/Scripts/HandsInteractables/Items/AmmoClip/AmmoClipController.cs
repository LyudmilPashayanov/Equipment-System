using Vertigo.Audio;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// This class contains the business logic of the Ammo-clip item
    /// </summary>
    public class AmmoClipController : ItemController<AmmoClipView>, ICombinableItem
    {
        #region Variables
        private AmmoClipModel _model;
        private bool _usable = true;

        public AmmoClipController(AmmoClipView view, AmmoClipModel model) : base(view)
        {
            _model = model;
            _usagesLeft = _model.usages;
        }

        #endregion
        #region Functionality

        public void TryCombineWithItemInOtherHand(IUsableItem otherItem)
        {
            if (_usable)
            {
                if (otherItem is GunController)
                {
                    GunController gun = (GunController)otherItem;
                    _view.ReloadAnimation(_model.reloadTime, gun.GetTransform(), () => ReloadGun(gun));
                    AudioManager.Instance.PlayOneShot(_model.gunReloadAudio);
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
            Destroy();
        }
        #endregion
    }
}
