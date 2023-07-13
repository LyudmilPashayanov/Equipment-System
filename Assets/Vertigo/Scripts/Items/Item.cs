using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    private const float PICK_UP_DURATION = 0.5f;

    [SerializeField] private Rigidbody _rb;

    Sequence _pickUpSequence;
       
    public void EquipItem(Transform _parent) 
    {
        Debug.Log("equipping item: " + gameObject.name);

        _rb.isKinematic = true;
        transform.SetParent(_parent.transform, true);
        Vector3 targetRotation = new Vector3(0,-90,0);
        _pickUpSequence = DOTween.Sequence();
        _pickUpSequence.Append(transform.DOLocalMove(Vector3.zero, PICK_UP_DURATION));
        _pickUpSequence.Insert(0,transform.DOLocalRotate(targetRotation, PICK_UP_DURATION));
    }

    public void UnequipItem()
    {
        Debug.Log("Unequip Item: " + gameObject.name);

        _rb.isKinematic = false;
        if (_pickUpSequence.active) 
        {
            _pickUpSequence.Kill();
        }
        transform.SetParent(null);
       
        // TODO:: apply force in the direction of which the hand is currently moving.
    }

}
