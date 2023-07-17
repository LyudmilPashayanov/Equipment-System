using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Interactables
{
    enum LeverState 
    {
        ungrabbed,
        grabbed,
        thresholdReached,
        successfulPull
    }

    public class LeverController : Grabable
    {
        private const float GOAL_VALUE = 155;
        private const float RELEASED_VALUE = 35;

        [SerializeField] private string _onThresholdReachedText = "release lever";
        [SerializeField] private string _onGrabbedText = "pull down";
        [SerializeField] private string _defaultText = "grab me";
        [SerializeField] private string _onSuccessText = "spawning item";
        [SerializeField] private LeverView _view;
        [SerializeField] private Transform _handlePivot;
        [SerializeField] private float _leverMovementSpeed = 100f;

        private Hand _holderHand;
        private bool _grabbed;
        private float _handsMovementY;
        private float _xRotation;

        private LeverState _currentState;
        private HashSet<Action> OnSuccessfulPullCallbacks = new HashSet<Action>();

        private void Start()
        {
            _view.Init(RELEASED_VALUE, GOAL_VALUE, _defaultText, _onGrabbedText, _onThresholdReachedText, _onSuccessText);
        }
       
        private void Update()
        {
            if (_grabbed)
            {
                MoveLever();
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
            _xRotation = Mathf.Clamp(_xRotation, RELEASED_VALUE, GOAL_VALUE+20);
            _handlePivot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            SetColorOnValueChange();
            SetStateOnValueChange();
        }

        public override void Grab(Hand hand)
        {
            _holderHand = hand;
            _grabbed = true;
        }

        public override void Release()
        {
            _grabbed = false;
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
    }
}