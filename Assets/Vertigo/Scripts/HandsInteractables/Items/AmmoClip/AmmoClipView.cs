using DG.Tweening;
using System;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// Handles all Unity and visual related logic for the Ammo-clip item. 
    /// </summary>
    public class AmmoClipView : ItemView
    {
        #region Variables
        private Sequence _flashingSequence;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private AmmoClipModel _model;
        private Color _originalColor;
        #endregion

        #region Functionality
        private void Start()
        {
            _originalColor = _meshRenderer.material.color;
        }
        
        public override void InitController()
        {
            Controller = new AmmoClipController(this, _model);
        }

        public void ReloadAnimation(float reloadTime, Transform goToTransform, Action onAnimationFinish)
        {
            transform.DOMove(goToTransform.position, reloadTime).SetEase(Ease.InSine);
            transform.DOScale(Vector3.zero, reloadTime).SetEase(Ease.InCubic).OnComplete(() => onAnimationFinish?.Invoke());
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
