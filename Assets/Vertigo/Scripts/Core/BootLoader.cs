using UnityEngine;
using Vertigo.UI;

namespace Vertigo
{
    public class BootLoader : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Application.targetFrameRate = 120;
        }

        private void Start()
        {
            _uiManager.FadeBackground();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape)) 
            {
                Application.Quit();
            }
        }
    }
}
