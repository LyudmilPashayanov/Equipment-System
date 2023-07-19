using DG.Tweening;
using System;
using UnityEngine;

namespace Vertigo.Player.Interactables.Weapons
{
    /// <summary>
    /// Handles all visual related logic for the Ammo-Clip. 
    /// </summary>
    public class AmmoClipView : ItemView
    {
        #region Functionality
        internal void ReloadAnimation(float reloadTime, Transform goToTransform, Action onAnimationFinish)
        {
            transform.DOMove(goToTransform.position, reloadTime).SetEase(Ease.InSine);
            transform.DOScale(Vector3.zero, reloadTime).SetEase(Ease.InCubic).OnComplete(() => onAnimationFinish?.Invoke());
        }
        #endregion
    }
}
