using UnityEngine;

namespace Player.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rb;
        [SerializeField] protected Collider _pickUpCollider;
        protected bool _itemEquipped;
        protected Hand _handHolder;
        public virtual void StartUse() { }
        public virtual void StopUse() { }
        public virtual void ToggleMode() { }

        public virtual void Equip(Hand Hand)
        {
            _itemEquipped = true;
            TogglePickUpCollider(false);
            ToggleKinematic(true);
            _handHolder = Hand;
        }

        public virtual void Unequip(Vector3 throwDirection, float throwForce)
        {
            _itemEquipped = false;
            ToggleKinematic(false);
            TogglePickUpCollider(true);
            ApplyThrowForce(throwDirection, throwForce);
            _handHolder = null;
        }

        private void TogglePickUpCollider(bool enable)
        {
            _pickUpCollider.enabled = enable;
        }

        protected void ToggleKinematic(bool enable)
        {
            _rb.isKinematic = enable;
        }

        protected void ApplyThrowForce(Vector3 throwDirection, float throwForce)
        {
            _rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }
}