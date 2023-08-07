using UnityEngine;
using TMPro;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// Handles all visual related logic of the Gun. 
    /// </summary>
    public class GunView : ItemView
    {
        #region Variables
        private const string AUTOMATIC_MODE_TEXT = "a";
        private const string SINGLE_MODE_TEXT = "s";

        [SerializeField] private TextMeshPro _remainingBulletsText;
        [SerializeField] private TextMeshPro _shootingModeText;
        [SerializeField] private GunModel _gunModel;
        [SerializeField] private BulletModel _bulletModel;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private AudioSource _gunAudioSource;
        #endregion

        #region Functionality

        public override void InitController()
        {
            Controller = new GunController(this, _gunModel, _bulletModel);
        }

        public void SetRemainingBullets(string remainingBullet)
        {
            _remainingBulletsText.text = remainingBullet;
        }

        public void ToggleAutomaticModeText(bool automaticEnabled)
        {
            _shootingModeText.text = automaticEnabled ? AUTOMATIC_MODE_TEXT : SINGLE_MODE_TEXT;
        }

        public void ShootBullet(Bullet bullet, float gunForce)
        {
            bullet.Shoot(_bulletSpawnPoint, gunForce);
        }

        public void PlayBulletSound() 
        {
           // _gunAudioSource.PlayOneShot(_gunModel.fireBulletSound);
        }
        #endregion
    }
}
