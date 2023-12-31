using System;
using System.Collections;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// The class handles the logic of the Bullet fired from the gun. 
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Rigidbody _bulletRigidbody;
        private Coroutine _despawnCoroutine;

        private event Action<Bullet> OnDespawn;

        private int _lifetime;
        private int _damage;
        #endregion

        #region Functionality
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
            SetBulletLifespan(_lifetime);
        }

        private void SetBulletLifespan(float lifetime)
        {
            _despawnCoroutine = StartCoroutine(DespawnBullet(lifetime));
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
                _despawnCoroutine = null;
            }
            gameObject.SetActive(false);
            OnDespawn?.Invoke(this);
        }
        #endregion

        #region Event Handlers
        private void OnCollisionEnter(Collision collision)
        {
            IHittable hittable;
            if (collision.collider.TryGetComponent<IHittable>(out hittable))
            {
                hittable.Hit(_damage);
                Despawn();
            }
        }
        #endregion
    }
}
