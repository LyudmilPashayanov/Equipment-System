using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

namespace Vertigo.UI
{
    /// <summary>
    /// This class serves as an orchestrator for the UI in the game.
    /// As the game is planned to be in VR, theere isn't much UI going on now.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image _background;
        #endregion

        #region Functionality

        private void Awake()
        {
            _background.color = Color.black;
        }

        public void FadeBackground()
        {
            _background.DOFade(0, 2f);
        }
        #endregion
    }
}