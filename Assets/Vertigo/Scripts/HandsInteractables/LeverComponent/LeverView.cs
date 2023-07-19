using TMPro;
using UnityEngine;


namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// This class handles all the visual changes of the Lever component.
    /// </summary>
    public class LeverView : MonoBehaviour
    {
        #region Variables

        private string _onThresholdReachedText;
        private string _onGrabbedText;
        private string _defaultText;
        private string _onSuccessText;

        [SerializeField] private TextMeshPro _leverText;
        [SerializeField] private Renderer _indicatorRenderer;
        [SerializeField] private Color _successColor = Color.green;

        private Color _originalColor;
        private float _minLeverX;
        private float _maxLeverX;
        #endregion 

        #region Functionality

        private void Start()
        {
            _originalColor = _indicatorRenderer.material.color;
        }

        internal void Init(float minLeverX, float maxLeverX, string defaultText, string onGrabbedText, string onThresholdReachedText, string onSuccessText)
        {
            _defaultText = defaultText;
            _onGrabbedText = onGrabbedText;
            _onThresholdReachedText = onThresholdReachedText;
            _onSuccessText = onSuccessText;
            _minLeverX = minLeverX;
            _maxLeverX = maxLeverX;
            _leverText.text = _defaultText;
        }
        
        public void ChangeOnSuccessfulText(string text) 
        {
            _onSuccessText = text;
        }
        
        public void ChangeOnGrabbedText(string text) 
        {
            _onGrabbedText = text;
        } 
        
        public void ChangeDefaultText(string text) 
        {
            _defaultText = text;
        }
        
        public void ChangeThresholdReachedText(string text) 
        {
            _onThresholdReachedText = text;
        }

        public void ChangeColorOnValue(float value)
        {
            Color targetColor = Color.Lerp(_originalColor, _successColor, Mathf.InverseLerp(_minLeverX, _maxLeverX, value));
            _indicatorRenderer.material.color = targetColor;
        }

        internal void UpdateText(LeverState currentState)
        {
            switch (currentState)
            {
                case LeverState.ungrabbed:
                    _leverText.text = _defaultText;
                    break;
                case LeverState.grabbed:
                    _leverText.text = _onGrabbedText;
                    break;
                case LeverState.thresholdReached:
                    _leverText.text = _onThresholdReachedText;
                    break;
                case LeverState.successfulPull:
                    _leverText.text = _onSuccessText;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}