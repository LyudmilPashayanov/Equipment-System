using System;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    [CreateAssetMenu(fileName = "GunModel", menuName = "Vertigo/Item Models/Gun Model")]
    [Serializable]
    public class GunModel : ScriptableObject
    {
        public int bulletSpeed;
        public float fireRate;
        public int magazineSize;
    }
}