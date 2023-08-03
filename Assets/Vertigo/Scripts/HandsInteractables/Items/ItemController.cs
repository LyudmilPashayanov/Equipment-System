using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all item controllers in the game. 
    /// </summary>

    public abstract class ItemController<TView> where TView : ItemView
    {
        #region Variables
        protected TView _view;
        #endregion

        #region Functionality

        public ItemController(TView view)
        {
            _view = view;
            _view.Updated += OnViewUpdate;
            _view.OnItemUse += UseItem;
            _view.OnItemStopUse += StopUseItem;
            _view.OnItemToggle += ToggleItem;
        }

        protected virtual void UseItem()
        { }

        protected virtual void StopUseItem()
        { }

        protected virtual void ToggleItem()
        { }

        protected virtual void Update()
        { }

        protected bool PlayAudio(string key)
        {
            return true;
            // play the audio needed if found
        }

        private void OnViewUpdate()
        {
            Update();
        }

        private AudioClip GetAudioClip(string key) // Get audio from model
        {
            return null;
            // have a dictionary with audio files and keys in the model
            // return FitmindService.GetMiniGameAudioClip(_content.Code, key);
        }


        /* protected Coroutine ExecuteDelayed(Action callback, float delay)
         {
             if (_view != null)
                 return _view.ExecuteDelayed(callback, delay);
             Debug.LogError("The view is null, make sure you don't use this method before the Start()");
             return null;

         }

         protected void StopCoroutine(Coroutine routine)
         {
             if (_view == null)
             {
                 Debug.LogError("The view is null, make sure you don't use this method before the Start()");
                 return;
             }
             if (routine != null)
                 _view.StopCoroutine(routine);
         }*/


        #endregion
    }
}