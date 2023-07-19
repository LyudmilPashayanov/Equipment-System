using UnityEngine;
using Vertigo.Audio;

namespace Vertigo.Player.Interactables.Weapons
{
    public class GunController : ItemController
    {
        [SerializeField] private GunView _view;
        [SerializeField] private GunModel _model;
        [SerializeField] private BulletModel _bulletModel;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private Bullet _bulletPrefab;
        private BulletFactory _bulletFactory;
       
        private float _gunForce;
        private float _fireRate;
        private int _magazineMaxSize;

        private bool _isShooting;
        private float _fireTimer;
        private int _currentMagazineSize;
        private bool _automaticShoot = true;

        private void Start()
        {
            _gunForce = _model.bulletSpeed;
            _fireRate = _model.fireRate;
            _magazineMaxSize = _model.magazineSize;
            _currentMagazineSize = _magazineMaxSize;
            _bulletFactory = new BulletFactory(_bulletPrefab, _bulletModel);

            _view.SetRemainingBullets(_currentMagazineSize.ToString());
            _view.ToggleAutomaticModeText(_automaticShoot);
        }

        private void Update()
        {
            if (_itemEquipped)
            {
                ShootingTick();
            }
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
            bullet.Shoot(_bulletSpawnPoint, _gunForce);
            AudioManager.Instance.PlayBulletSound();
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

        public void ReloadBullets(int bulletsBeingReloaded)
        {
            _currentMagazineSize += bulletsBeingReloaded;
            if (_currentMagazineSize > _magazineMaxSize)
                _currentMagazineSize = _magazineMaxSize;
            _view.SetRemainingBullets(_currentMagazineSize.ToString());
        }

        public override void ToggleMode()
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
        }
    }
}