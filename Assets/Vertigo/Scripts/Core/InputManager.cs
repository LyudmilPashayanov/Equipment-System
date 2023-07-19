using UnityEngine;

namespace Vertigo.Core
{
    /// <summary>
    /// This class is needed so that the "Unity Input System" can registed Input given from the user.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        #region Variables

        public static InputManager Instance;
        #endregion

        #region Functionality

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}
