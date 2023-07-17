using UnityEngine;

namespace Player.Interactables
{
    public class Lever : Grabable
    {
        [SerializeField] private Transform _handlePivot;
        [SerializeField] private float _leverMovementSpeed = 100f;
        private bool _grabbed;
        private Hand _holderHand;

        private float _handsMovementY;
        private float _xRotation;

        private void Update()
        {
            if (_grabbed)
            {
                MoveLever();
            }
        }
      
        private void MoveLever() 
        {
            _handsMovementY = Time.deltaTime * _leverMovementSpeed * _holderHand.GetMovementDirection(false).z;
            _xRotation -= _handsMovementY;
            _xRotation = Mathf.Clamp(_xRotation, 35f, 175f);
            _handlePivot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        }

        public override void Grab(Hand hand)
        {
            _holderHand = hand;
            _grabbed = true;
        }

        public override void Release()
        {
            _grabbed = false;
        }
    }
}