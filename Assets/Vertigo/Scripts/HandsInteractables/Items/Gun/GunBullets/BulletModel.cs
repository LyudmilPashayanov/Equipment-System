using System;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    [CreateAssetMenu(fileName = "BulletModel", menuName = "Vertigo/Item Models/Bullet Model")]
    [Serializable]
    /// <summary>
    /// This class serves as a data holder for the bullet.
    /// </summary>
    public class BulletModel : ScriptableObject
    {
        public int lifetime;
        public int damage;
        public Bullet bulletPrefab;
    }
}