using UnityEngine;

namespace Vertigo.Player.Interactables
{
    public class FlashlightController : ItemController
    {
        [SerializeField] FlashlightView _view;

        private bool _isFlashOn;
        public override void StartUse(Hand handUsingIt)
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