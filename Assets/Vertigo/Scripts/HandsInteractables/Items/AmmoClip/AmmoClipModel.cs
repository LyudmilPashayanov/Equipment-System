using System;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    [CreateAssetMenu(fileName = "AmmoClipModel", menuName = "Vertigo/Item Models/Ammo-clip Model")]
    [Serializable]
    /// <summary>
    /// This class serves as a data holder for the Ammo-clip item.
    /// </summary>
    public class AmmoClipModel : ScriptableObject
    {
        public int ammoCount;
        public float reloadTime;
        public AudioClip gunReloadAudio;

    }
}
