using System;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all items in the game. 
    /// </summary>
    public interface IUsableItem
    {
        public void StartUse();
        public void StopUse();
        public void ToggleItem();
        public void Release();
        public void SetParent(Transform parent, bool worldPosStays=false);
        public void AddThrowForce(Vector3 throwDirection, float force);
        public void Destroy();
        public int GetUsagesLeft();
        
    }

    public abstract class ItemController<TView> : IUsableItem where TView : IItemView
    {
        #region Variables
        protected TView _view;
        protected int _usagesLeft = -1;
        #endregion

        #region Functionality
        public ItemController(TView view)
        {
            _view = view;
            _view.OnUpdate += ViewUpdate;
        }
        private void ViewUpdate()
        {
            Update();
        }

        protected virtual void Update() { }

        public virtual void StartUse()
        {
            _usagesLeft--;
        }

        public virtual void Release()
        { 
            _view.Release();
        }

        public virtual void SetParent(Transform parent, bool worldPosStays = false)
        {
            _view.ParentItem(parent, worldPosStays);
        }

        public virtual void AddThrowForce(Vector3 throwDirection, float force)
        {
            _view.ApplyThrowForce(throwDirection, force);
        }

        public virtual int GetUsagesLeft() 
        {
            return _usagesLeft;
        }
        public virtual void Destroy()
        {
            _view.Destroy();
        }
        public virtual void StopUse() { }
        public virtual void ToggleItem() { }
  

        #endregion
    }
}