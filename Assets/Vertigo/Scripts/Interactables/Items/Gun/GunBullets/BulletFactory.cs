using System.Collections.Generic;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    public class BulletFactory
    {
        private Bullet _bulletPrefab;
        private BulletModel _bulletModel;
        private GameObject _bulletParent;
        private Queue<Bullet> _bulletPool = new Queue<Bullet>();

        public BulletFactory(Bullet BulletPrefab, BulletModel bulletModel)
        {
            _bulletParent = new GameObject("BulletsParent");
            _bulletPrefab = BulletPrefab;
            _bulletModel = bulletModel;
        }

        public Bullet GetBullet()
        {
            if (_bulletPool.Count == 0)
            {
                InstantiateBullet();
            }
            Bullet bullet = _bulletPool.Dequeue();
            bullet.SubscribeOnDespawnEvent(ReturnToPool);
            return bullet;
        }

        private void InstantiateBullet()
        {
            GameObject bulletGO = Object.Instantiate(_bulletPrefab.gameObject);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Init(_bulletModel);
            ReturnBulletToPool(bullet);
        }

        private void ReturnBulletToPool(Bullet bullet)
        {
            bullet.transform.SetParent(_bulletParent.transform);
            _bulletPool.Enqueue(bullet);
        }

        private void ReturnToPool(Bullet bullet)
        {
            ReturnBulletToPool(bullet);
        }
    }
}