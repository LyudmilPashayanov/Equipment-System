using DG.Tweening;
using TMPro;
using UnityEngine;
using Vertigo.Audio;

namespace Vertigo.Minigames
{
    /// <summary>
    /// This class handles the logic of the Shoot the Target minigame.
    /// </summary>
    public class TargetPracticeMinigame : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TargetPractice _target;
        [SerializeField] private TextMeshPro _scoreText;
        [SerializeField] private Transform _posA;
        [SerializeField] private Transform _posB;
        [SerializeField] private AudioClip _targetHitSound;

        private int _score;
        private Sequence _moveTargetSequence;
        #endregion 

        #region Functionality
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
            AudioSource.PlayClipAtPoint(_targetHitSound, transform.position);
        }

        private void UpdateScoreText()
        {
            _scoreText.text = _score.ToString();
        }
        #endregion 
    }
}
