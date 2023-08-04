using System;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class for all item controllers in the game. 
    /// </summary>
    public abstract class ItemController
    {
        public virtual void StartUse(Hand handUsingIt) { }
        public virtual void StopUse() { }
        public virtual void ToggleItem() { }
        public virtual void ReleaseItem() { }
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

        protected bool PlayAudio(string key)
        {
            return true;
            // play the audio needed if found
        }

        private void ViewUpdate()
        {
            Update();
        }

        private AudioClip GetAudioClip(string key) // Get audio from model
        {
            return null;
            // have a dictionary with audio files and keys in the model
            // return FitmindService.GetMiniGameAudioClip(_content.Code, key);
        }
        #endregion
    }
}