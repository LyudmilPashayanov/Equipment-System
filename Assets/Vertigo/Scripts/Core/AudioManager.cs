using UnityEngine;

namespace Vertigo.Audio 
{
    /// <summary>
    /// Audio manager, which is singleton, so that you can play audio from anywhere in the app you want.
    /// </summary> 
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }

        [SerializeField] AudioSource _oneShotSource;
        [SerializeField] AudioSource _radioSource;

        [SerializeField] AudioClip _fireBulletSound;
        [SerializeField] AudioClip _targetHitSound;
        [SerializeField] AudioClip _toggleItemModeSound;
        [SerializeField] AudioClip _gunReloadAudio;
        [SerializeField] AudioClip _leverPullAudio;
        [SerializeField] AudioClip _cantinaBand;

        void Awake()
        {
            _instance = this;
        }

        public void PlayBulletSound()
        {
            _oneShotSource.PlayOneShot(_fireBulletSound);
        }

        public void PlayToggleModeSound()
        {
            _oneShotSource.PlayOneShot(_toggleItemModeSound);
        }

        public void PlayTargetHitSound()
        {
            _oneShotSource.PlayOneShot(_targetHitSound);
        }

        public void PlayAmmoReloadSound()
        {
            _oneShotSource.PlayOneShot(_gunReloadAudio);
        }
        
        public void PlayLeverPulledSound()
        {
            _oneShotSource.PlayOneShot(_leverPullAudio);
        }

        public void PlayCantinaBand(bool enable)
        {
            if (enable)
            {
                _radioSource.PlayOneShot(_cantinaBand);
            }
            else
            {
                _radioSource.Stop();
            }
        }
    }
}
