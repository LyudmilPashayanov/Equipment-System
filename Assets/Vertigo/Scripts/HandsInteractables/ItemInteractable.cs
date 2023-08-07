using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class which marks all items which can be grabbed.
    /// </summary>
    /// 
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected Collider _InteractableCollider;
        public abstract void Release();
    }

    public abstract class ItemInteractable : Interactable
    {
        public abstract ItemController Grab(Hand handHolder);
    }

    public abstract class StaticObjectInteractable : Interactable
    {
        public abstract void Grab(Hand handHolder);
    }


}