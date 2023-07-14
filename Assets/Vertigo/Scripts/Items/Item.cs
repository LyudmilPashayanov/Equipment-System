using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    protected bool _itemEquipped;
    public abstract void StartUse();
    public abstract void StopUse();
    public abstract void ToggleMode();

    public virtual void Equip() 
    {
        _itemEquipped = true;
        ToggleKinematic(true);
    }
    
    public virtual void Unequip(Vector3 throwDirection, float throwForce) 
    {
        _itemEquipped = false;
        ToggleKinematic(false);
        ApplyThrowForce(throwDirection, throwForce);
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
