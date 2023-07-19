using System;

namespace Vertigo
{
    /// <summary>
    /// Marks an object in the game as one that the bullets would hit and inflict damage to.
    /// </summary>
    public interface IHittable
    {
        public event Action<int> OnHit;
        public void Hit(int damage);
    }
}

