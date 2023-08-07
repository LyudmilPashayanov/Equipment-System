using System.Collections.Generic;
using UnityEngine;

namespace Vertigo.Audio
{
    /// <summary>
    /// This C# class is created and used from the <see cref="AudioManager"/> and is responsible for 3D Audio Sources creation and pooling them when needed.
    /// </summary>
    public class SpatialAudioSourceFactory
    {
        #region Variables

        private Queue<AudioSource> _audioSourcePool = new Queue<AudioSource>();
        private Transform _parent;
        private AudioSource _audioSourcePrefab;
        #endregion

        #region Functionality

        public SpatialAudioSourceFactory(Transform audioSourcesParent, AudioSource audioSourcePrefab)
        {
            _parent = audioSourcesParent;
            _audioSourcePrefab = audioSourcePrefab;
        }

        public AudioSource GetAudioSourceFromPool()
        {
            // Check if there's an available audio source in the pool
            if (_audioSourcePool.Count > 0)
            {
                AudioSource source = _audioSourcePool.Dequeue();
                return source;
            }

            // If no available audio source found, create a new one and add it to the pool
            var newAudioSourceObject = Object.Instantiate(_audioSourcePrefab, _parent);
            return newAudioSourceObject;
        }

        public void ReturnAudioSourceToPool(AudioSource audioSource)
        {
            _audioSourcePool.Enqueue(audioSource);
        }
        #endregion
    }
}