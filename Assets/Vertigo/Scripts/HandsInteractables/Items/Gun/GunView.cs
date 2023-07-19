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
        #endregion

        #region Functionality
        public void SetRemainingBullets(string remainingBullet)
        {
            _remainingBulletsText.text = remainingBullet;
        }

        public void ToggleAutomaticModeText(bool automaticEnabled)
        {
            _shootingModeText.text = automaticEnabled ? AUTOMATIC_MODE_TEXT : SINGLE_MODE_TEXT;
        }
        #endregion
    }
}
