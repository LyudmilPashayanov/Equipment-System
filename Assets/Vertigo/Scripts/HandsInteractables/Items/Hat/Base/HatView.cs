using DG.Tweening;
using UnityEngine;
using Vertigo.Player;
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

    public override ItemController Grabbed(HandInput Hand)
    {
        base.Grabbed(Hand);
        _onHeadCollider.enabled = false;
        // Controller.OnUnequipped?.Invoke(); //TODO : Maybe handle that tthrough an event raised from the equipment manager to a listener in the controller
        return Controller;
    }
}
