using DG.Tweening;
using UnityEngine;
using Vertigo.Player.Interactables;

public abstract class HatView : ItemView
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] protected Collider _onHeadCollider;

    private Sequence _flashingSequence;
    private Color _originalColor;

    private void Start()
    {
        _originalColor = _meshRenderer.material.color;
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

    public override IUsableItem GrabItem()
    {
        base.GrabItem();
        _onHeadCollider.enabled = false;
        return Controller;
    }
}
