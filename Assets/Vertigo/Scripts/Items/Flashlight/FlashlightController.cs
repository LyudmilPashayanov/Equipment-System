using UnityEngine;

namespace Player.Items
{
    public class FlashlightController : Item
    {
        [SerializeField] FlashlightView _view;

        private bool _isFlashOn;
        public override void StartUse()
        {
            _isFlashOn = !_isFlashOn;
            _view.ToggleFlashlight(_isFlashOn);
        }

        public override void ToggleMode()
        {
            _view.ChangeIntensity();
        }
    }
}