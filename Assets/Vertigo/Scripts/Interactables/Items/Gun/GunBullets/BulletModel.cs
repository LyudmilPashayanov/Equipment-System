using System;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    [CreateAssetMenu(fileName = "BulletModel", menuName = "Vertigo/Item Models/Bullet Model")]
    [Serializable]
    public class BulletModel : ScriptableObject
    {
        public int lifetime;
        public int damage;
    }
}