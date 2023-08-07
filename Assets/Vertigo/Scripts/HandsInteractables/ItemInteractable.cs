using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Interface which marks all objects in the game which the <see cref="Hand"/> can interact with.
    /// </summary>
    public interface IInteractable
    {
        public abstract void Release();
    }

    /// <summary>
    /// Base class which marks all items which the <see cref="Hand"/> can interact with.
    /// </summary>
    public abstract class ItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Collider _interactableCollider;
        public abstract IUsableItem GetUsableItem();
        public abstract void Release();
    }

    /// <summary>
    /// Base class which marks static objects which the <see cref="Hand"/> can interact with.
    /// </summary>
    public abstract class StaticInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Collider _interactableCollider;
        public abstract void RegisterHand(IHand hand);
        public abstract void Release();
    }


}