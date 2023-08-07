using System;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all items in the game. 
    /// </summary>
    public abstract class ItemController
    {
        public virtual void StartUse() { }
        public virtual void StopUse() { }
        public virtual void ToggleItem() { }
        public abstract void Release();
        public abstract void SetParent(Transform parent, bool worldPosStays=false);

        public abstract void AddThrowForce(Vector3 throwDirection, float force);
    }

    public abstract class ItemController<TView> : ItemController where TView : IItemView
    {
        #region Variables
        protected TView _view;
        #endregion

        #region Functionality
        public ItemController(TView view)
        {
            _view = view;
            _view.OnUpdate += ViewUpdate;
        }

        protected virtual void Update()
        { }

        private void ViewUpdate()
        {
            Update();
        }

        public override void Release()
        { 
            _view.Release();
        }

        public override void SetParent(Transform parent, bool worldPosStays = false)
        {
            _view.ParentItem(parent, worldPosStays);
        }

        public override void AddThrowForce(Vector3 throwDirection, float force)
        {
            _view.ApplyThrowForce(throwDirection, force);
        }

        #endregion
    }
}