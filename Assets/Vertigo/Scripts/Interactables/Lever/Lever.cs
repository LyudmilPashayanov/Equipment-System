using UnityEngine;

namespace Player.Interactables
{
    public class Lever : Grabable
    {
        [SerializeField] private Transform _handlePivot;
        [SerializeField] private float _leverMovementSpeed = 100f;
        private bool _grabbed;
        private Hand _holderHand;
        public int topBorder = 100;
        private void Update()
        {
            if (_grabbed)
            {
                float rotationAngle = Time.deltaTime * Vector3.Dot(_holderHand.GetMovementDirection(false), Vector3.back) * _leverMovementSpeed;
                Quaternion rotation = Quaternion.Euler(rotationAngle, 0f, 0f);
                _handlePivot.rotation *= rotation;
            }
        }
        private Quaternion ClampRotation(Quaternion rotation, float minAngle, float maxAngle)
        {
            Vector3 euler = rotation.eulerAngles;
            euler.x = Mathf.Clamp(euler.x, minAngle, maxAngle);
            return Quaternion.Euler(euler);
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