using System.Collections.Generic;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// This C# class is used in from the GunController and is responsible for Bullet creation and pooling them when needed.
    /// </summary>
    public class BulletFactory
    {
        #region Variables
        private Bullet _bulletPrefab;
        private BulletModel _bulletModel;
        private GameObject _bulletParent;
        private Queue<Bullet> _bulletPool = new Queue<Bullet>();
        #endregion

        #region Functionality
        public BulletFactory(BulletModel bulletModel)
        {
            _bulletParent = new GameObject("BulletsParent");
            _bulletPrefab = bulletModel.bulletPrefab;
            _bulletModel = bulletModel;
        }

        public Bullet GetBullet()
        {
            if (_bulletPool.Count == 0)
            {
                InstantiateBullet();
            }
            Bullet bullet = _bulletPool.Dequeue();
            return bullet;
        }

        private void InstantiateBullet()
        {
            GameObject bulletGO = Object.Instantiate(_bulletPrefab.gameObject);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.SubscribeOnDespawnEvent(ReturnToPool);
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
        #endregion
    }
}