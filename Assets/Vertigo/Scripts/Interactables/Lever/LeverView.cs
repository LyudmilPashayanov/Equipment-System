using TMPro;
using UnityEngine;
namespace Player.Interactables
{
    public class LeverView : Grabable
    {
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

        public override void Grab(Hand Hand)
        {
            throw new System.NotImplementedException();
        }

        public override void Release()
        {
            throw new System.NotImplementedException();
        }
    }
}