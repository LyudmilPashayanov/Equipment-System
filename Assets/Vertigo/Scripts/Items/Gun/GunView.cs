using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class GunView : MonoBehaviour
{
    private const string AUTOMATIC_MODE_TEXT = "a";
    private const string SINGLE_MODE_TEXT = "s";

    [SerializeField] private TextMeshPro _remainingBulletsText;
    [SerializeField] private TextMeshPro _shootingModeText;
    
    public void SetRemainingBullets(string remainingBullet) 
    {
        _remainingBulletsText.text = remainingBullet;
    }

    public void ToggleAutomaticModeText(bool automaticEnabled) 
    {
            _shootingModeText.text = automaticEnabled ? AUTOMATIC_MODE_TEXT : SINGLE_MODE_TEXT;
    }
}
