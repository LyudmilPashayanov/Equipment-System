using System;

namespace Player.Interactables
{
    public abstract class ItemController
    {
        public abstract void Subscribe(Action onUsed, Action onStoppedUse, Action onToggle);
    }
}