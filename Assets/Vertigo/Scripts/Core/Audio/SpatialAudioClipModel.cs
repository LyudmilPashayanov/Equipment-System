using System;
using UnityEngine;

namespace Vertigo.Audio
{
    [CreateAssetMenu(fileName = "SpatialAudioClipModel", menuName = "Vertigo/Audio Models/AudioClip Model")]
    [Serializable]
    /// <summary>
    /// This class serves as a data holder for the loudness and audio clip which is supposed to be played in 3D.
    /// </summary>
    public class SpatialAudioClipModel : ScriptableObject
    {
        public int loudness;
        public AudioClip audioClip;

    }
}
