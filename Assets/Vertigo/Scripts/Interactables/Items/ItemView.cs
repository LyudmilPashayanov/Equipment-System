using UnityEngine;
using DG.Tweening;

namespace Player.Interactables
{
    public abstract class ItemView : Grabable
    {
        private const float PICK_UP_DURATION = 0.5f;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] protected Rigidbody _rb;
        protected Hand _handHolder;

        private Sequence _flashingSequence;
        private Color _originalColor;
        private Sequence _pickUpSequence;

        public virtual void StartUse(Hand handUsingIt) { }
        public virtual void StopUse() { }
        public virtual void ToggleMode() { }

        private void Start()
        {
            _originalColor = _meshRenderer.material.color;
        }

        public override void Grab(Hand Hand)
        {
            _handHolder = Hand;
            SubscribeHand();
            ToggleKinematic(true);

            transform.SetParent(Hand.GetPalm(), true);
            _pickUpSequence = DOTween.Sequence();
            _pickUpSequence.Append(transform.DOLocalMove(Vector3.zero, PICK_UP_DURATION));
            _pickUpSequence.Insert(0, transform.DOLocalRotate(new Vector3(0, -90, 0), PICK_UP_DURATION));
        }

        public override void Release()
        {
            if (_pickUpSequence.active)
            {
                _pickUpSequence.Kill();
            }
            UnsubscribeHand();
            transform.SetParent(null);
            ToggleKinematic(false);
            ApplyThrowForce(_handHolder.GetMovementDirection(),_handHolder.GetHandStrength());
            _handHolder = null;
        }

        public void SubscribeHand()
        {
            _handHolder.Subscribe(StartUse, StopUse, ToggleMode);
        }

        public void UnsubscribeHand()
        {
            _handHolder.Unsubscribe(StartUse, StopUse, ToggleMode);
        }

        public virtual void UnusableIndication()
        {
            if (_flashingSequence != null && _flashingSequence.active)
            {
                return;
            }
            _flashingSequence = DOTween.Sequence();
            _flashingSequence.Append(_meshRenderer.material.DOColor(Color.red, 0.2f));
            _flashingSequence.Append(_meshRenderer.material.DOColor(_originalColor, 0.2f));
            _flashingSequence.Append(_meshRenderer.material.DOColor(Color.red, 0.2f));
            _flashingSequence.Append(_meshRenderer.material.DOColor(_originalColor, 0.2f));
        }

        protected void ToggleKinematic(bool enable)
        {
            _rb.isKinematic = enable;
        }

        protected void ApplyThrowForce(Vector3 handMovementDirection, float throwForce)
        {
            Vector3 throwDirection = Quaternion.Euler(0f, _handHolder.transform.parent.rotation.eulerAngles.y, 0f) * handMovementDirection;
            _rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }
}