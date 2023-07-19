using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class which marks all items which can be grabbed.
    /// </summary>
    public abstract class Grabbable : MonoBehaviour
    {
        [SerializeField] protected Collider _interactableCollider;
        public abstract void Grab(Hand Hand);
        public abstract void Release();
    }
}