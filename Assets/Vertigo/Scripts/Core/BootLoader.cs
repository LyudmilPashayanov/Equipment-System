using UnityEngine;
using Vertigo.UI;

namespace Vertigo
{
    /// <summary>
    /// This class is the first thing that runs in the game.
    /// TODO: It will be responsible to spawn the needed Level and the Player in that level.
    /// Also if needed inject the classes with one another, so that they can
    /// hold the references they need. Currently not having any persistancy in the game, makes this class almost obsolete.
    /// </summary>
    public class BootLoader : MonoBehaviour
    {
        #region Variables
        [SerializeField] private UIManager _uiManager;
        #endregion

        #region Functionality

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
        #endregion
    }
}
