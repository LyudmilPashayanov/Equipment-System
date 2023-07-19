using UnityEngine;
using Vertigo.Player.Interactables;

namespace Vertigo.Audio
{
    public class Radio : MonoBehaviour
    {
        [SerializeField] private LeverController _lever;
        [SerializeField] private string _successfulText = "";

        private bool _playing;

        private void Start()
        {
            _lever.AddListener(StartRadio);
            if (_successfulText != "")
                _lever.ChangeOnSuccessfulText(_successfulText);
        }

        private void StartRadio()
        {
            AudioManager.Instance.PlayCantinaBand(!_playing);
            _playing = !_playing;
        }
    }
}
