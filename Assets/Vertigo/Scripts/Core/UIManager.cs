using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

namespace Vertigo.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image _background;

        private void Awake()
        {
            _background.color = Color.black;
        }

        public void FadeBackground()
        {
            _background.DOFade(0, 2f);
        }
    }
}