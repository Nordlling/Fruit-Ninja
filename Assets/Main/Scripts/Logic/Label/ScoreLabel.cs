using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Logic.Label
{
    public class ScoreLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreValue;
        [SerializeField] private float _timeBeforeAnimation;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _animationFadeSpeed;
        [SerializeField] private float _animationMoveOffset;
        
        private ITimeProvider _timeProvider;
        private Sequence _sequence;

        public void Construct(string value, ITimeProvider timeProvider)
        {
            _scoreValue.text = value;
            _timeProvider = timeProvider;
        }
        private void Start()
        {
            PlayAnimation();
        }

        private void Update()
        {
            _sequence.timeScale = _timeProvider.GetTimeScale();
        }

        private void PlayAnimation()
        {
            _sequence = DOTween.Sequence()
                .AppendInterval(_timeBeforeAnimation)
                .Append(
                    transform.DOMoveY(transform.position.y + _animationMoveOffset, _animationDuration))
                .Join(
                    DOTween.To(() => _scoreValue.alpha, x => _scoreValue.alpha = x, 0f, _animationFadeSpeed)
                        .OnComplete(() => Destroy(gameObject)));

            _sequence.Play();
        }
    }
}