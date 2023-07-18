using UnityEngine;

namespace Vertigo.Player.Interactables
{
    public abstract class Grabable : MonoBehaviour
    {
        [SerializeField] protected Collider _interactableCollider;
        public abstract void Grab(Hand Hand);
        public abstract void Release();
    }
}