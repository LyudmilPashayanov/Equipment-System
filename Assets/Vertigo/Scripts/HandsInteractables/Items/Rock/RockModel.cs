using System;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    [CreateAssetMenu(fileName = "RockModel", menuName = "Vertigo/Item Models/Rock Model")]
    [Serializable]
    /// <summary>
    /// This class serves as a data holder for the Rock item.
    /// </summary>
    public class RockModel : ScriptableObject
    {
        public int Usages = 1;
    }
}
