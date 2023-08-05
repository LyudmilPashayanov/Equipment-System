namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all items in the game. 
    /// </summary>
    public abstract class ItemController
    {
        public virtual void StartUse(Hand handUsing) { }
        public virtual void StopUse() { }
        public virtual void ToggleItem() { }
        public virtual void ReleaseItem(Hand handReleasing) { }
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
        #endregion
    }
}