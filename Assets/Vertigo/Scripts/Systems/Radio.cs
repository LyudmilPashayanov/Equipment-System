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
        [SerializeField] private SpatialAudioClipModel _radioClipModel;

        private bool _playing = false;
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
            _playing = !_playing;
            if (_playing)
            {
                AudioManager.Instance.PlayAtPoint(_radioClipModel.audioClip, transform.position,_radioClipModel.loudness);
            }
            else
            {
                AudioManager.Instance.StopSpatialAudioSource(_radioClipModel.audioClip);
            }
        }
        #endregion
    }
}
