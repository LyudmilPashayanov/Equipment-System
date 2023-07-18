using UnityEngine;

namespace Vertigo
{
    public class BootLoader : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("Boot loader");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Application.targetFrameRate = 120;
        }
    }
}
