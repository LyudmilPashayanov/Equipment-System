using UnityEngine;
using Vertigo.Audio;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// This class contains the business and Unity logic of the Ammo-clip item as a Grabbable game object.
    /// </summary>
    public class AmmoClipController : ItemController, ICombinableItem
    {
        #region Variables

        [SerializeField] private AmmoClipView _view;
        [SerializeField] private AmmoClipModel _model;
        private bool _usable = true;
        #endregion
        #region Functionality

        public void TryCombineWithItemInOtherHand(Grabbable otherItem)
        {
            if (_usable)
            {
                if (otherItem is GunController)
                {
                    GunController gun = (GunController)otherItem;
                    _view.ReloadAnimation(_model.reloadTime, gun.transform, () => ReloadGun(gun));
                    AudioManager.Instance.PlayAmmoReloadSound();
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
        #endregion
    }
}
