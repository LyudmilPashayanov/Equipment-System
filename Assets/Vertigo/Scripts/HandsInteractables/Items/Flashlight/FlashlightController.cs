using UnityEngine;
using Vertigo.Audio;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// This class contains the business logic of the Flashlight item as a Grabbable game object.
    /// </summary>
    public class FlashlightController : ItemController
    {
        #region Variables
        [SerializeField] FlashlightView _view;

        private bool _isFlashOn;
        #endregion 


        #region Functionality
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
        #endregion

    }
}