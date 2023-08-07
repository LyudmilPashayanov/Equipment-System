using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vertigo.Player.Interactables
{
    enum LeverState 
    {
        ungrabbed,
        grabbed,
        thresholdReached,
        successfulPull
    }

    /// <summary>
    /// Lever Component which can be used for any type of functionality as an Input Device. 
    /// Just AddListener() and it will Invoke your added event, when the player pulls the lever.
    /// </summary>
    public class LeverController : StaticInteractable
    {
        #region Variables
        private const float GOAL_VALUE = 155;
        private const float RELEASED_VALUE = 35;
        [SerializeField] private LeverView _view;
        [SerializeField] private Transform _handlePivot;
        [SerializeField] private AudioClip _leverPullAudio;

        [Header("Default Label indicators:")]
        [SerializeField] private string _defaultText = "grab me";
        [SerializeField] private string _onGrabbedText = "pull down";
        [SerializeField] private string _onThresholdReachedText = "release lever";
        [SerializeField] private string _onSuccessText = "spawning item";

        private float _leverMovementSpeed = 5f;

        private IHand _holderHand;
        private bool _grabbed;

        private float _handsMovementY;
        private float _xRotation;

        private LeverState _currentState;
        private HashSet<Action> OnSuccessfulPullCallbacks = new HashSet<Action>();
        #endregion

        #region Functionality
        private void Start()
        {
            _view.Init(RELEASED_VALUE, GOAL_VALUE, _defaultText, _onGrabbedText, _onThresholdReachedText, _onSuccessText);
        }

        private void Update()
        {
            if (_grabbed)
            {
                MoveLever();
                //TODO: CheckRangeFromHand, if far away- unequip
            }
        }

        public void SetEnabled(bool enable) 
        {
            _interactableCollider.enabled = enable;        
        }

        private void MoveLever() 
        {
            _handsMovementY = Time.deltaTime * _leverMovementSpeed * _holderHand.GetMovementDirection(false).z;
            _xRotation -= _handsMovementY;
            _xRotation = Mathf.Clamp(_xRotation, RELEASED_VALUE, GOAL_VALUE + 20);
            _handlePivot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            SetColorOnValueChange();
            SetStateOnValueChange();
        }

        // Should be in the View
        public override void RegisterHand(IHand hand)
        {
            _holderHand = hand;
            _grabbed = true;
            SetEnabled(false);
        }

        public override void Release()
        {
            _grabbed = false;
            _holderHand = null;
            SetEnabled(true);
            OnLeverRelease();
        }

        private void SetColorOnValueChange() 
        {
            _view.ChangeColorOnValue(_xRotation);
        }
            
        private void SetStateOnValueChange() 
        {
            if (_xRotation > GOAL_VALUE)
            {
                UpdateState(LeverState.thresholdReached);
            }
            else if (_xRotation < GOAL_VALUE)
            {
                UpdateState(LeverState.grabbed);
            }
        }

        private void OnLeverRelease() 
        {
            if(_xRotation > GOAL_VALUE) 
            {
                OnSuccessfulPull();
            }
            else 
            {
                UpdateState(LeverState.ungrabbed);
            }
        }

        private void OnSuccessfulPull() 
        {
            UpdateState(LeverState.successfulPull);
            InvokeCallbacks();
            //AudioManager.Instance.PlaySoundAtPoint(_leverPullAudio,transform.position);
            ReturnLeverToDefault();
        }

        private void UpdateState(LeverState state) 
        {
            _currentState = state;
            _view.UpdateText(_currentState);
        }

        private void ReturnLeverToDefault() 
        {
            SetEnabled(false);
            Sequence seq = DOTween.Sequence();
            seq.Append(_handlePivot.transform.DOLocalRotateQuaternion(Quaternion.Euler(RELEASED_VALUE, 0, 0), 0.5f));
            seq.Insert(0,DOTween.To(() => _xRotation, x => _xRotation = x, RELEASED_VALUE, 0.5f));
            seq.AppendInterval(1.5f).OnComplete(() => 
            {
                SetEnabled(true);
                SetColorOnValueChange();
                UpdateState(LeverState.ungrabbed);
            });
        }

        public void ChangeOnSuccessfulText(string text)
        {
            _view.ChangeOnSuccessfulText(text);
        }

        public void ChangeOnGrabbedText(string text)
        {
            _view.ChangeOnGrabbedText(text);
        }

        public void ChangeDefaultText(string text)
        {
            _view.ChangeDefaultText(text);
        }

        public void ChangeThresholdReachedText(string text)
        {
            _view.ChangeThresholdReachedText(text);
        }

        private void InvokeCallbacks() 
        {
            foreach (Action callback in OnSuccessfulPullCallbacks)
            {
                callback?.Invoke();
            }
        }

        public void AddListener(Action onSuccessfulPullCallback)
        {
            OnSuccessfulPullCallbacks.Add(onSuccessfulPullCallback);
        }

        public void RemoveListener(Action onSuccessfulPullCallback)
        {
            OnSuccessfulPullCallbacks.Remove(onSuccessfulPullCallback);
        }

        public void RemoveAllListener()
        {                
            OnSuccessfulPullCallbacks.Clear();
        }
        #endregion
    }
}