using UnityEngine;

namespace Player.Interactables
{
    public class FlashlightController : ItemController
    {
        [SerializeField] FlashlightView _view;

        private bool _isFlashOn;
        public override void OnStartUse(Hand handUsingIt)
        {
            _isFlashOn = !_isFlashOn;
            _view.ToggleFlashlight(_isFlashOn);
        }

        public override void OnToggleMode()
        {
            _view.ChangeIntensity();
        }
    }
}