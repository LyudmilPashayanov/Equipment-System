using DG.Tweening;
using System;
using UnityEngine;

public class AmmoClipView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    private Sequence _flashingSequence;

    internal void ReloadAnimation(float reloadTime, Transform goToTransform, Action onAnimationFinish)
    {
        transform.DOMove(goToTransform.position, reloadTime).SetEase(Ease.InSine);
        transform.DOScale(Vector3.zero, reloadTime).SetEase(Ease.InCubic).OnComplete(()=> onAnimationFinish?.Invoke());
    }

    internal void UnusableIndication()
    {
        if (_flashingSequence!= null && _flashingSequence.active)
        {
            _flashingSequence.Kill();
        }
        Color original = _meshRenderer.material.color;
        _flashingSequence = DOTween.Sequence();
        _flashingSequence.Append(_meshRenderer.material.DOColor(Color.red,0.2f));
        _flashingSequence.Append(_meshRenderer.material.DOColor(original, 0.2f));
        _flashingSequence.Append(_meshRenderer.material.DOColor(Color.red, 0.2f));
        _flashingSequence.Append(_meshRenderer.material.DOColor(original, 0.2f));
    }
}
