using UnityEngine;
using TMPro;

namespace Player.Interactables
{
    public class GunView : ItemView
    {
        private const string AUTOMATIC_MODE_TEXT = "a";
        private const string SINGLE_MODE_TEXT = "s";

        [SerializeField] private TextMeshPro _remainingBulletsText;
        [SerializeField] private TextMeshPro _shootingModeText;
/*        [SerializeField] private GunView _view;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private float _fireRate = 0.333f; // 3 bullets per second

        private void Start()
        {
            GunController gunController = new GunController(this, _bulletSpawnPoint, _bulletFactory, _bulletSpeed, _fireRate);     
        }*/

        public void SetRemainingBullets(string remainingBullet)
        {
            _remainingBulletsText.text = remainingBullet;
        }

        public void ToggleAutomaticModeText(bool automaticEnabled)
        {
            _shootingModeText.text = automaticEnabled ? AUTOMATIC_MODE_TEXT : SINGLE_MODE_TEXT;
        }
    }
}
