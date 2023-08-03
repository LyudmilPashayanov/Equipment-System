using UnityEngine;
using TMPro;
using System;

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

        GunController _controller; // maybe a reference is not needed here.
        #endregion

        #region Functionality

        private void Awake()
        {
            _controller = new GunController(this, _gunModel, _bulletModel);
        }

        public void SetRemainingBullets(string remainingBullet)
        {
            _remainingBulletsText.text = remainingBullet;
        }

        public void ToggleAutomaticModeText(bool automaticEnabled)
        {
            _shootingModeText.text = automaticEnabled ? AUTOMATIC_MODE_TEXT : SINGLE_MODE_TEXT;
        }

        public override void Grab(Hand _currentHand)
        {
            base.Grab(_currentHand);
        }

        public override void Release()
        {
            base.Release();
            StopUse(); // stops shooting if the item is released
        }

        public void ShootBullet(Bullet bullet, float gunForce)
        {
            bullet.Shoot(_bulletSpawnPoint, gunForce);
        }

        #endregion
    }
}
