using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _maxBulletCount = 15;

    private Queue<GameObject> _bulletPool;
    private Transform _bulletParent;

    private void Awake()
    {
        _bulletPool = new Queue<GameObject>(_maxBulletCount);
        _bulletParent = new GameObject("BulletParent").transform;

        for (int i = 0; i < _maxBulletCount; i++)
        {
            GameObject bullet = InstantiateBullet();
            ReturnBulletToPool(bullet);
        }
    }

    private GameObject InstantiateBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletParent);
        bullet.SetActive(false);
        return bullet;
    }

    public GameObject GetBullet()
    {
        if (_bulletPool.Count > 0)
        {
            GameObject bullet = _bulletPool.Dequeue();
            bullet.SetActive(true);
            StartCoroutine(DelayedReturnToPool(bullet, 3f));
            return bullet;
        }
        else
        {
            return InstantiateBullet();
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(_bulletParent);
        _bulletPool.Enqueue(bullet);
    }

    private System.Collections.IEnumerator DelayedReturnToPool(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnBulletToPool(bullet);
    }
}
