using System;
using UnityEngine;

namespace Vertigo.Minigames
{
    public class TargetPractice : MonoBehaviour, IHittable
    {
        public event Action<int> OnHit;

        public void Hit(int damage)
        {
            OnHit?.Invoke(damage);
        }
    }
}
