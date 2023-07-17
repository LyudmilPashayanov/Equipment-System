using System;
using UnityEngine;

namespace Player.Interactables
{
    [CreateAssetMenu(fileName = "AmmoClipModel", menuName = "Vertigo/Item Models/Ammo-clip Model")]
    [Serializable]
    public class AmmoClipModel : ScriptableObject
    {
        public int ammoCount;
        public float reloadTime;
    }
}
