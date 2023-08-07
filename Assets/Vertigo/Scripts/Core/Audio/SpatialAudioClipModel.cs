using System;
using UnityEngine;

namespace Vertigo.Audio
{
    [CreateAssetMenu(fileName = "SpatialAudioClipModel", menuName = "Vertigo/Audio Models/AudioClip Model")]
    [Serializable]
    /// <summary>
    /// This class serves as a data holder for the Ammo-clip item.
    /// </summary>
    public class SpatialAudioClipModel : ScriptableObject
    {
        public int loudness;
        public AudioClip audioClip;

    }
}
