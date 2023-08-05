using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Vertigo.Audio 
{
    /// <summary>
    /// Audio manager, which is singleton, so that you can play audio from anywhere in the app you want.
    /// </summary> 

    public class AudioManager : MonoBehaviour
    {
        #region Variables
        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }

        [SerializeField] AudioSource _playerSFXAudioSource;
        [SerializeField] AudioSource _backgroundAudioSource;

        [SerializeField] AudioClip _backgroundMusic;
        #endregion

        #region Functionality
        void Awake()
        {
            _instance = this;
        }

        public void PlaySound(AudioClip clipToPlay)
        {
            _playerSFXAudioSource.PlayOneShot(clipToPlay);
        }

        public void PlaySoundAtPoint(AudioClip clipToPlay, Vector3 point) 
        {
            AudioSource.PlayClipAtPoint(clipToPlay, point);
        }

        public void PlayBackgroundMusic(bool enable)
        {
            if (enable)
            {
                _backgroundAudioSource.PlayOneShot(_backgroundMusic);
            }
            else
            {
                _backgroundAudioSource.Stop();
            }
        }
        #endregion
    }
}
