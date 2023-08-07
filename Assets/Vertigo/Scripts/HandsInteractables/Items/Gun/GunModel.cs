using System;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    [CreateAssetMenu(fileName = "GunModel", menuName = "Vertigo/Item Models/Gun Model")]
    [Serializable]

    /// <summary>
    /// This class serves as a data holder for the Gun item.
    /// </summary>
    public class GunModel : ScriptableObject
    {
        public int bulletSpeed;
        public float fireRate;
        public int magazineSize;
        public AudioClip fireBulletSound;
        public AudioClip toggleItemModeSound;

    }
}