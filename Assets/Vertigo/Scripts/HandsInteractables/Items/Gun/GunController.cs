using UnityEngine;
using Vertigo.Audio;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// This class contains the business and Unity logic of the Gun item as a Grabbable game object.
    /// </summary>
    public class GunController : ItemController<GunView>
    {

        #region Variables
        private GunModel _model;
        private BulletModel _bulletModel;

        private BulletFactory _bulletFactory;
       
        private float _gunForce;
        private float _fireRate;
        private int _magazineMaxSize;

        private bool _isShooting;
        private float _fireTimer;
        private int _currentMagazineSize;
        private bool _automaticShoot = true;

        public GunController(GunView view, GunModel model, BulletModel bulletModel) : base(view)
        {
            _model = model;
            _bulletModel = bulletModel;
            _bulletFactory = new BulletFactory(_bulletModel);

            _gunForce = _model.bulletSpeed;
            _fireRate = _model.fireRate;
            _magazineMaxSize = _model.magazineSize;
            _currentMagazineSize = _magazineMaxSize;

            _view.SetRemainingBullets(_currentMagazineSize.ToString());
            _view.ToggleAutomaticModeText(_automaticShoot);
        }
        #endregion

        #region Functionality

        protected override void Update()
        {    
            ShootingTick();
        }

        private void ShootingTick()
        {
            if (_fireTimer > 0)
            {
                _fireTimer -= Time.deltaTime;
            }
            if (_isShooting && _fireTimer <= 0 && _currentMagazineSize > 0)
            {
                FireBullet();
                _fireTimer = _fireRate;
            }
        }

        private void FireBullet()
        {
            Bullet bullet = _bulletFactory.GetBullet();
            _view.ShootBullet(bullet, _gunForce);
           // TODO: Play the audio from the gun model- AudioManager.Instance.PlayBulletSound();
            _view.SetRemainingBullets((--_currentMagazineSize).ToString());
            if (!_automaticShoot)
            {
                StopShooting();
            }
        }
        
        protected override void UseItem()
        {
            StartShooting();
        }

        protected override void ToggleItem()
        {
            _automaticShoot = !_automaticShoot;
            AudioManager.Instance.PlayToggleModeSound();
            _view.ToggleAutomaticModeText(_automaticShoot);
        }

        protected override void StopUseItem()
        {
            StopShooting();
        }

        private void StartShooting()
        {
            _isShooting = true;
        }

        private void StopShooting()
        {
            _isShooting = false;
        }

        public void ReloadBullets(int bulletsBeingReloaded)
        {
            _currentMagazineSize += bulletsBeingReloaded;
            if (_currentMagazineSize > _magazineMaxSize)
                _currentMagazineSize = _magazineMaxSize;
            _view.SetRemainingBullets(_currentMagazineSize.ToString());
        }
        #endregion

        #region Event Handlers
        /*public override void ToggleMode()
        {
            _automaticShoot = !_automaticShoot;
            AudioManager.Instance.PlayToggleModeSound();
            _view.ToggleAutomaticModeText(_automaticShoot);
        }

        public override void StartUse(Hand handUsingIt)
        {
            StartShooting();
        }

        public override void StopUse()
        {
            StopShooting();
        }

        public override void Grab(Hand _currentHand)
        {
            base.Grab(_currentHand);
        }

        public override void Release()
        {
            base.Release();
            StopShooting();
        }*/
        #endregion
    }
}