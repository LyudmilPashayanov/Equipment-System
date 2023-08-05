using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class which marks all items which can be grabbed.
    /// </summary>
    public abstract class Grabbable : MonoBehaviour
    {
        [SerializeField] protected Collider _grabbableCollider;
        public abstract ItemController Grabbed(HandInput Hand); // Maybe make Grabbable Item and only Grabbable to distinguish from the Lever
        public abstract void Release();
    }
}