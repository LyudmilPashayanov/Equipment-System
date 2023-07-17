using UnityEngine;

namespace Player.Interactables
{
    public class FlashlightView : ItemView
    {
        private enum FlashIntensity
        {
            low = 0,
            medium = 1,
            high = 2
        }

        [SerializeField] private Light _light;
        [SerializeField] private MeshRenderer _onIndicator;
        [SerializeField] private Material _offMaterial;
        [SerializeField] private Material _onMaterial;

        private FlashIntensity _currentIntensity;
        internal void ToggleFlashlight(bool enable)
        {
            _light.enabled = enable;
            _onIndicator.material = enable ? _onMaterial : _offMaterial;
        }

        internal void ChangeIntensity()
        {
            _currentIntensity = _currentIntensity + 1 > FlashIntensity.high ? 0 : _currentIntensity + 1;
            switch (_currentIntensity)
            {
                case FlashIntensity.low:
                    _light.intensity = 50;
                    break;
                case FlashIntensity.medium:
                    _light.intensity = 100;
                    break;
                case FlashIntensity.high:
                    _light.intensity = 150;
                    break;
            }
        }
    }
}