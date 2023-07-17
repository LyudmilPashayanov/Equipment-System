using UnityEngine;
using TMPro;
using System;

namespace Player.Interactables
{
    [CreateAssetMenu(fileName = "GunModel", menuName = "Vertigo/Item Models/Gun Model")]
    [Serializable]
    public class GunModel : ScriptableObject
    {
        public int bulletSpeed;
        public float fireRate;
        public int magazineSize;
    }

    public class GunView : ItemView
    {
        private const string AUTOMATIC_MODE_TEXT = "a";
        private const string SINGLE_MODE_TEXT = "s";

        [SerializeField] private TextMeshPro _remainingBulletsText;
        [SerializeField] private TextMeshPro _shootingModeText;
        [SerializeField] private GunView _view;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private GunModel _model;
        private GunController _gunController;

        Action OnStartedUsing;
        Action OnStoppedUsing;
        Action OnToggleMode;

        private void Start()
        {
            _gunController = new GunController(this, _bulletSpawnPoint, _model.bulletSpeed,_model.fireRate,_model.magazineSize);
            _gunController.Subscribe(OnStartedUsing, OnStoppedUsing, OnToggleMode);
            
            SetRemainingBullets(_gunController.Magazine.ToString());
            ToggleAutomaticModeText(_gunController.AutomaticShoot);
            

        }

        public void SetRemainingBullets(string remainingBullet)
        {
            _remainingBulletsText.text = remainingBullet;
        }

        public void ToggleAutomaticModeText(bool automaticEnabled)
        {
            _shootingModeText.text = automaticEnabled ? AUTOMATIC_MODE_TEXT : SINGLE_MODE_TEXT;
        }

        public override void Release()
        {
            base.Release();
            _controller.StopShooting();
        }

        public override void ToggleMode()
        {
            OnToggleMode?.Invoke();
            AutomaticShoot = !AutomaticShoot;
            ToggleAutomaticModeText(AutomaticShoot);
        }

        public override void StartUse(Hand handUsingIt)
        {
            OnStartedUsing?.Invoke();
        }

        public override void StopUse()
        {
            OnStoppedUsing?.Invoke();
        }
    }
}
