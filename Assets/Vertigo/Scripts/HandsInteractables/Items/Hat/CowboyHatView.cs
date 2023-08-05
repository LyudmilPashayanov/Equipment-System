using DG.Tweening;
using System;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    public class CowboyHatView : ItemView
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] protected Collider _onHeadCollider;

        private Sequence _flashingSequence;
        private Color _originalColor;

        public event Action OnUnequipped;

        private void Start()
        {
            _originalColor = _meshRenderer.material.color;
        }
        
        internal void EnableOnHeadCollider(bool enalbe)
        {
            _onHeadCollider.enabled = enalbe;
        }

        public virtual void UnusableIndication() // maybe move this to the view.
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

        public override ItemController Grab(Hand Hand)
        {
            base.Grab(Hand);
            _onHeadCollider.enabled = false;
            OnUnequipped?.Invoke();
            return Controller;
        }

        public override void InitController()
        {
            Controller = new CowboyHatController(this);
        }
    }
}