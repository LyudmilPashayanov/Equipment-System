using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class which marks all items which can be interacted with.
    /// </summary>
    /// 
    public interface IInteractable
    {
        public abstract void Release();
    }

    public abstract class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Collider _interactableCollider;
        public abstract IUsableItem GetUsableItem();
        public abstract void Release();
    }

    public abstract class StaticInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Collider _interactableCollider;
        public abstract void RegisterHand(IHand hand);
        public abstract void Release();
    }


}