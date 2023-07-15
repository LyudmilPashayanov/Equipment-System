using Player;
using UnityEngine;

public class Head : MonoBehaviour
{
    private GameObject _equippedHat;
    
    internal bool TryEquipHat(IHat hatItem)
    {
        if (_equippedHat == null)
        {
            _equippedHat = hatItem.GetGameObject();
            _equippedHat.transform.SetParent(transform, false);    
            return true;
        }
        else
            return false;
    }

    public void UnequipHat(Hand grabbingHand)
    {
        _equippedHat.transform.SetParent(grabbingHand.transform, false);
        _equippedHat = null;
    }
}
