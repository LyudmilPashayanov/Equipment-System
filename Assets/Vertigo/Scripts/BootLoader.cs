using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Vertigo
{
    public class BootLoader : MonoBehaviour
    {
        [SerializeField] private Image _background;

        private void Awake()
        {
            _background.color = Color.black;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Application.targetFrameRate = 120;
        }

        private void Start()
        {
            FadeBackground();
        }

        private void FadeBackground() 
        {
            _background.DOFade(0, 2f);
        }

    }
}
