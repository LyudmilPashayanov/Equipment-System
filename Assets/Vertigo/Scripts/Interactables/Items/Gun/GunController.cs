using System;
using UnityEngine;

namespace Player.Interactables
{
    public class GunController : ItemController
    {
        private GunView _view;
        private Transform _bulletSpawnPoint;
        private BulletFactory _bulletFactory;
        private float _bulletSpeed;
        private float _fireRate;

        private bool _isShooting;
        private float _fireTimer;
        public int _magazineSize;

        public bool AutomaticShoot {get; set;}
        public int Magazine { get; set; }

        public GunController(GunView View, Transform BulletSpawnPoint, float BulletSpeed, float FireRate,int MagazineSize)
        {
            _view = View;
            _bulletSpawnPoint = BulletSpawnPoint;
            _bulletSpeed = BulletSpeed;
            _fireRate = FireRate;
            _magazineSize = MagazineSize;
            _bulletFactory = new BulletFactory();
            AutomaticShoot = true;
        }

        public override void Subscribe(Action onUsed, Action onStoppedUse, Action onToggle)
        {
            onUsed += StartShooting;
            onStoppedUse += StopShooting;
            onToggle += ToggleAutomaticMode;
        }

        private void Update()
        {
            if (_handHolder != null)
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
            if (_isShooting && _fireTimer <= 0 && Magazine > 0)
            {
                FireBullet();
                _fireTimer = _fireRate;
            }
        }

        private void FireBullet()
        {
            GameObject bullet = _bulletFactory.GetBullet();
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = bullet.transform.forward * _bulletSpeed;
            }
            _view.SetRemainingBullets((--Magazine).ToString());
            if (!AutomaticShoot)
            {
                StopShooting();
            }
        }

        private void StartShooting()
        {
            _isShooting = true;
        }

        public void StopShooting()
        {
            _isShooting = false;
        }

        public void ReloadBullets(int bulletsBeingReloaded)
        {
            Magazine += bulletsBeingReloaded;
            if (Magazine > _magazineSize)
                Magazine = _magazineSize;
            _view.SetRemainingBullets(Magazine.ToString());
        }

        public void ToggleAutomaticMode()
        {
            AutomaticShoot = !AutomaticShoot;
        }

        public override void OnStartUse(Hand handUsingIt)
        {
            StartShooting();
        }

        public override void OnStopUse()
        {
            StopShooting();
        }
    }
}