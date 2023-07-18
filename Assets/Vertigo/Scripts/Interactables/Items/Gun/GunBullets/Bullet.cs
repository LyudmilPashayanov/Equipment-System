using System;
using System.Collections;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _bulletRigidbody;
        private Coroutine _despawnCoroutine;

        private event Action<Bullet> OnDespawn;

        private int _lifetime;
        private int _damage;

        public int GetDamage()
        {
            return _damage;
        }

        public void Init(BulletModel model)
        {
            _lifetime = model.lifetime;
            _damage = model.damage;
        }

        public void Shoot(Transform SpawnPoint, float gunForce)
        {
            gameObject.SetActive(true);
            transform.position = SpawnPoint.position;
            transform.rotation = SpawnPoint.rotation;
            _bulletRigidbody.velocity = transform.forward * gunForce;
            Debug.Log("_lifetime " + _lifetime);
            SetBulletLifespan(_lifetime);
        }

        private void SetBulletLifespan(float lifetime)
        {
            _despawnCoroutine = StartCoroutine(DespawnBullet(lifetime));
            Debug.Log("StartCoroutine Bullet Despawning");
        }

        private IEnumerator DespawnBullet(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            _despawnCoroutine = null;
            Despawn();
        }

        public void SubscribeOnDespawnEvent(Action<Bullet> callback)
        {
            OnDespawn += callback;
        }

        public void Despawn()
        {
            if (_despawnCoroutine != null)
            {
                StopCoroutine(_despawnCoroutine);
            }
            Debug.Log("Bullet Despawning");
            gameObject.SetActive(false);
            OnDespawn?.Invoke(this);
        }
    }
}
