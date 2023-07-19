using UnityEngine;
using Vertigo.Audio;

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
            AudioManager.Instance.PlayToggleModeSound();
        }
    }
}