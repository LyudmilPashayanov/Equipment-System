using System.Collections.Generic;
using UnityEngine;

namespace Vertigo.Audio
{
    public class SpatialAudioSourceFactory
    {
        #region Variables

        private Queue<AudioSource> _audioSourcePool = new Queue<AudioSource>();
        private Transform _parent;

        #endregion

        #region Functionality

        public SpatialAudioSourceFactory(Transform audioSourcesParent)
        {
            _parent = audioSourcesParent;
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
            GameObject newAudioSourceObject = new GameObject();
            newAudioSourceObject.transform.SetParent(_parent);
            AudioSource newAudioSource = newAudioSourceObject.AddComponent<AudioSource>();
            newAudioSource.spatialBlend = 1;
            return newAudioSource;
        }

        public void ReturnAudioSourceToPool(AudioSource audioSource)
        {
            _audioSourcePool.Enqueue(audioSource);
        }
        #endregion
    }
}