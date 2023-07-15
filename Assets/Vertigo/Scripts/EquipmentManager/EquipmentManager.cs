using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Hand _leftHand;
    [SerializeField] private Hand _rightHand;

    public Item GetOtherHandItem(Hand _currentHand)
    {
        return (_currentHand == _leftHand ? _rightHand : _leftHand).GetItem();
    }
}
