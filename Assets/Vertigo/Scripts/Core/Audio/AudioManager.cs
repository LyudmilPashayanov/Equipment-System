using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vertigo.Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }

        [SerializeField] AudioSource _oneShotSource;

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
            _spatialAudioSourceFactory = new SpatialAudioSourceFactory(transform);
        }

        public void PlayOneShot(AudioClip clipToPlay)
        {
            _oneShotSource.PlayOneShot(clipToPlay);
        }

        /// <summary>
        /// Optimized version with pooling technique of the AudioSource.PlayClipAtPointPlays 3D sound from the provided vector3 position.
        /// </summary>
        /// <param name="loudness">Specifies at what distance the sound will be the least hearable</param>
        public void PlayAtPoint(AudioClip clipToPlay, Vector3 positionToPlay, float loudness)
        {
            AudioSource audioSource = _spatialAudioSourceFactory.GetAudioSourceFromPool();
            audioSource.gameObject.name = clipToPlay.name;
            audioSource.transform.position = positionToPlay;
            audioSource.maxDistance = loudness;
            audioSource.clip = clipToPlay;
            audioSource.Play();
            StartCoroutine(ReturnAudioSourceToPoolAfterPlaying(audioSource, clipToPlay.length));
        }

        /// <summary>
        /// Stops a running spatial audio source. For example, if the radio is playing, you can call this function to stop it.
        /// </summary>
        /// <param name="clipToStop"></param>
        public void StopSpatialAudioSource(AudioClip clipToStop) 
        {
            foreach (KeyValuePair<AudioSource, Coroutine> item in _spatialSourcesPlaying)
            {
                AudioSource audioSource = item.Key;
                if(audioSource == clipToStop) 
                {
                    audioSource.Stop();
                    _spatialAudioSourceFactory.ReturnAudioSourceToPool(audioSource);
                    StopCoroutine(item.Value);
                }
            }
        }

        private IEnumerator ReturnAudioSourceToPoolAfterPlaying(AudioSource audioSource, float time)
        {
            yield return new WaitForSeconds(time);
            _spatialAudioSourceFactory.ReturnAudioSourceToPool(audioSource);
        }

        #endregion
    }
}