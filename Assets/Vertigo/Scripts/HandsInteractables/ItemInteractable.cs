using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class which marks all items which can be interacted with.
    /// </summary>
    /// 
    public interface IInteractable
    {
        public abstract IUsableItem GrabItem();
        public abstract void Release();
    }

    public abstract class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Collider _interactableCollider;
        public abstract IUsableItem GrabItem();
        public abstract void Release();
    }

/*    public abstract class StaticObjectInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Collider _InteractableCollider;
        public abstract IInteractable Grab(IHand handHolder);
        public abstract void Release();
    }*/


}