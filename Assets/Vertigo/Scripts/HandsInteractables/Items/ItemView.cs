using UnityEngine;
using DG.Tweening;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all Item Views in the game, handling all the visual changes of the items.
    /// </summary>
    public abstract class ItemView : MonoBehaviour
    {
        #region Variables
        [SerializeField] private MeshRenderer _meshRenderer;

        private Sequence _flashingSequence;
        private Color _originalColor;
        #endregion
        
        #region Functionality
        private void Start()
        {
            _originalColor = _meshRenderer.material.color;
        }

        public virtual void UnusableIndication()
        {
            if (_flashingSequence != null && _flashingSequence.active)
            {
                return;
            }
            _flashingSequence = DOTween.Sequence();
            _flashingSequence.Append(_meshRenderer.material.DOColor(Color.red, 0.2f));
            _flashingSequence.Append(_meshRenderer.material.DOColor(_originalColor, 0.2f));
            _flashingSequence.Append(_meshRenderer.material.DOColor(Color.red, 0.2f));
            _flashingSequence.Append(_meshRenderer.material.DOColor(_originalColor, 0.2f));
        }
#endregion
    }
}