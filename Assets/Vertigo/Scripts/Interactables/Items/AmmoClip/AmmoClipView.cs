using DG.Tweening;
using System;
using UnityEngine;

namespace Player.Interactables
{
    public class AmmoClipView : ItemView
    {
        internal void ReloadAnimation(float reloadTime, Transform goToTransform, Action onAnimationFinish)
        {
            transform.DOMove(goToTransform.position, reloadTime).SetEase(Ease.InSine);
            transform.DOScale(Vector3.zero, reloadTime).SetEase(Ease.InCubic).OnComplete(() => onAnimationFinish?.Invoke());
        }
    }
}
