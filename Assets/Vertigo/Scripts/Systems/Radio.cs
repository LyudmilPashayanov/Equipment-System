using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Audio
{
    /// <summary>
    /// Simple system to turn the radio in the game on when the Lever component is pulled down. 
    /// </summary>
    public class Radio : MonoBehaviour
    {
        #region Variables
        [SerializeField] private LeverController _lever;
        [SerializeField] private string _successfulText = "";

        private bool _playing;
        #endregion
        #region Functionality

        private void Start()
        {
            _lever.AddListener(StartRadio);
            if (_successfulText != "")
                _lever.ChangeOnSuccessfulText(_successfulText);
        }

        private void StartRadio()
        {
            AudioManager.Instance.PlayBackgroundMusic(!_playing);
            _playing = !_playing;
        }
        #endregion
    }
}
