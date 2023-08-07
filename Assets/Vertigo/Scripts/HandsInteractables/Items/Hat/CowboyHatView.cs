using DG.Tweening;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Handles all Unity and visual related logic for the Cowboy hat item. 
    /// </summary>
    public class CowboyHatView : ItemView
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] protected Collider _onHeadCollider;

        private Sequence _flashingSequence;
        private Color _originalColor;

        private void Start()
        {
            _originalColor = _meshRenderer.material.color;
        }

        public override void InitController()
        {
            Controller = new CowboyHatController(this);
        }

        public void EnableOnHeadCollider(bool enable)
        {
            _onHeadCollider.enabled = enable;
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

        public override IUsableItem GetUsableItem()
        {
            base.GetUsableItem();
            _onHeadCollider.enabled = false;
            return Controller;
        }
    }
}