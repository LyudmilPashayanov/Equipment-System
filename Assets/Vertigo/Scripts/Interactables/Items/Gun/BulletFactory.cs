using System.Collections.Generic;
using UnityEngine;

namespace Player.Interactables
{
    public class BulletFactory
    {
        [SerializeField] private GameObject _bulletPrefab;

        private GameObject _bulletParent;
        private Queue<GameObject> _bulletPool = new Queue<GameObject>();

        private void Start()
        {
            _bulletParent = new GameObject("BulletsParent");
        }

        public GameObject GetBullet()
        {
            if (_bulletPool.Count == 0)
            {
                InstantiateBullet();
            }
            GameObject bullet = _bulletPool.Dequeue();
            bullet.SetActive(true);
            StartCoroutine(DelayedReturnToPool(bullet, 3f));
            return bullet;
        }

        private void InstantiateBullet()
        {
            GameObject bullet = Object.Instantiate(_bulletPrefab);
            ReturnBulletToPool(bullet);
        }

        private void ReturnBulletToPool(GameObject bullet)
        {
            bullet.SetActive(false);
            bullet.transform.SetParent(_bulletParent.transform);
            _bulletPool.Enqueue(bullet);
        }

        private System.Collections.IEnumerator DelayedReturnToPool(GameObject bullet, float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnBulletToPool(bullet);
        }
    }
}