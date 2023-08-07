using System;
using UnityEngine;

namespace Vertigo.Minigames
{
    /// <summary>
    /// The target which the moves. Marked as <see cref="IHittable"/> so that the bullets hit it.
    /// </summary>
    public class TargetPractice : MonoBehaviour, IHittable
    {
        public event Action<int> OnHit;

        public void Hit(int damage)
        {
            OnHit?.Invoke(damage);
        }
    }
}
