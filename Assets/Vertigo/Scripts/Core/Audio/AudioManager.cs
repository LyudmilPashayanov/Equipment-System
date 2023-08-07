using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vertigo.Audio
{
    /// <summary>
    /// Singleton Audio manager so that you have a centralized place to control the SFX in your game.
    /// </summary> 
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }

        [SerializeField] private AudioSource _oneShotSource;
        [SerializeField] private AudioSource _3DEnvironmentSourcePrefab;

        private SpatialAudioSourceFactory _spatialAudioSourceFactory;
        private Dictionary<AudioSource, Coroutine> _spatialSourcesPlaying = new Dictionary<AudioSource, Coroutine>();
        #endregion

        #region Functionality
        void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            _spatialAudioSourceFactory = new SpatialAudioSourceFactory(transform, _3DEnvironmentSourcePrefab);
        }

        public void PlayOneShot(AudioClip clipToPlay)
        {
            _oneShotSource.PlayOneShot(clipToPlay);
        }

        /// <summary>
        /// Optimized version with pooling technique of the Unity version of <see cref="AudioSource.PlayClipAtPoint(AudioClip, Vector3)"/> 3D sound from the provided vector3 position.
        /// </summary>
        /// <param name="loudness">Specifies at what distance the sound will be the least hearable</param>
        public AudioSource PlayAtPoint(AudioClip clipToPlay, Vector3 positionToPlay, float loudness)
        {
            AudioSource audioSource = _spatialAudioSourceFactory.GetAudioSourceFromPool();
            audioSource.transform.position = positionToPlay;
            audioSource.maxDistance = loudness;
            audioSource.clip = clipToPlay;
            audioSource.Play();
            Coroutine returnToPoolCoroutine = StartCoroutine(ReturnAudioSourceToPoolAfterPlaying(audioSource, clipToPlay.length));
            _spatialSourcesPlaying.Add(audioSource, returnToPoolCoroutine);
            return audioSource;
        }

        /// <summary>
        /// Stops a running spatial audio source. For example, if the radio is playing, you can call this function to stop it.
        /// </summary>
        /// <param name="clipToStop"></param>
        public void StopSpatialAudioSource(AudioClip clipToStop) 
        {
            AudioSource sourceToRemove = null;
            foreach (KeyValuePair<AudioSource, Coroutine> item in _spatialSourcesPlaying)
            {
                AudioSource audioSource = item.Key;
                if(audioSource.clip == clipToStop) 
                {
                    sourceToRemove = audioSource;
                    audioSource.Stop();
                    _spatialAudioSourceFactory.ReturnAudioSourceToPool(audioSource);
                    StopCoroutine(item.Value);
                    break;
                }
            }
            if (sourceToRemove != null)
            {
                _spatialSourcesPlaying.Remove(sourceToRemove);
            }
        }

        private IEnumerator ReturnAudioSourceToPoolAfterPlaying(AudioSource audioSource, float time)
        {
            yield return new WaitForSeconds(time);
            _spatialSourcesPlaying.Remove(audioSource);
            _spatialAudioSourceFactory.ReturnAudioSourceToPool(audioSource);
        }

        #endregion
    }
}