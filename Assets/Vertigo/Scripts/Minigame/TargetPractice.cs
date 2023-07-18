using System;
using UnityEngine;

namespace Vertigo.Minigames
{
    public interface IHittable 
    {
        public event Action<int> OnHit;
        public void Hit(int damage);
    }

    public class TargetPractice : MonoBehaviour, IHittable
    {
        public event Action<int> OnHit;

        public void Hit(int damage)
        {
            OnHit?.Invoke(damage);
            Debug.Log("being hit for " + damage +" dmg.");
        }
    }
}
