using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Vertigo.Minigames
{
    public class TargetPracticeMinigame : MonoBehaviour
    {
        [SerializeField] private TargetPractice _target;
        [SerializeField] TextMeshPro _scoreText;
        [SerializeField] Transform _posA;
        [SerializeField] Transform _posB;

        private int _score;
        private Sequence _moveTargetSequence;
        
        private void Start()
        {
            _target.OnHit += TargetHit;
            MoveTarget();
        }

        private void MoveTarget() 
        {
            _moveTargetSequence = DOTween.Sequence();
            _target.transform.position = _posA.position;
            _moveTargetSequence.Append(_target.transform.DOMove(_posB.position, 2f));
            _moveTargetSequence.Append(_target.transform.DOMove(_posA.position, 2f));
            _moveTargetSequence.SetLoops(-1);
        }

        private void TargetHit(int damageDone)
        {
            _score += damageDone;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            _scoreText.text = _score.ToString();
        }
    }
}
