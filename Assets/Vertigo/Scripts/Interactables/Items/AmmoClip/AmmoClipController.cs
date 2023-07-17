using UnityEngine;

namespace Player.Interactables
{
    public interface ICombinableItemm 
    {
        public void TryCombineWithItem(Grabable otherItem);
    }

    public class AmmoClipController : ItemController, ICombinableItemm
    {
        [SerializeField] private AmmoClipView _view;
        [SerializeField] private AmmoClipModel _model;
        private bool _usable = true;

        public void TryCombineWithItem(Grabable otherItem)
        {
            if (_usable)
            {
                if (otherItem is GunController)
                {
                    GunController gun = (GunController)otherItem;
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
}
