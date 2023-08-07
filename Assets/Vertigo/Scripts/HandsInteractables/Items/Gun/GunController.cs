using UnityEngine;

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
            _view.PlayBulletSound();
            _view.SetRemainingBullets((--_currentMagazineSize).ToString());
            if (!_automaticShoot)
            {
                StopShooting();
            }
        }

        private void StartShooting()
        {
            _isShooting = true;
        }

        private void StopShooting()
        {
            _isShooting = false;
        }

        public Transform GetTransform() 
        {
            return _view.transform;
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
        public override void StartUse()
        {
            base.StartUse();
            StartShooting();
        }

        public override void StopUse()
        {
            StopShooting();
        }

        public override void ToggleItem()
        {
            _automaticShoot = !_automaticShoot;
            //AudioManager.Instance.PlaySound(); // Play shared toggle sound
            _view.ToggleAutomaticModeText(_automaticShoot);
        }

        public override void Release()
        {
            StopShooting();
            base.Release();
        }
        #endregion
    }
}