using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public void ToggleKinematic(bool enable) 
    {
        _rb.isKinematic = enable;
    }

    public void ApplyThrowForce(Vector3 throwDirection, float throwForce) 
    {
        _rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
