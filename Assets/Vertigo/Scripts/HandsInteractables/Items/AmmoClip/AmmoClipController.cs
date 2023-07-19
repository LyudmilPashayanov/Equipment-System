using UnityEngine;
using Vertigo.Audio;

namespace Vertigo.Player.Interactables.Weapons
{
    public class AmmoClipController : ItemController, ICombinableItem
    {
        [SerializeField] private AmmoClipView _view;
        [SerializeField] private AmmoClipModel _model;
        private bool _usable = true;

        public void TryCombineWithItemInOtherHand(Grabable otherItem)
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
    }
}
