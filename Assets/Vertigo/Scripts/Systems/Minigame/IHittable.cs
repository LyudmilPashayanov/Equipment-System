using System;

namespace Vertigo
{
    public interface IHittable
    {
        public event Action<int> OnHit;
        public void Hit(int damage);
    }
}

