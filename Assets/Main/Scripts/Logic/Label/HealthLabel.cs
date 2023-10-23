using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Logic.Label
{
    public class HealthLabel : MonoBehaviour
    {
        [SerializeField] private float _timeBeforeAnimation;
        [SerializeField] private float _timeAfterAnimation;
        [SerializeField] private float _animationDuration;
        
        private ITimeProvider _timeProvider;
        private Sequence _sequence;
        private Vector2 _targetPosition;

        public void Construct(ITimeProvider timeProvider, Vector2 targetPosition)
        {
            _timeProvider = timeProvider;
            _targetPosition = targetPosition;
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
                .Append(transform.DOMove(_targetPosition, _animationDuration))
                .Append(transform.DOScale(0f, _timeAfterAnimation))
                .OnKill(() => Destroy(gameObject))
                .Play();
        }
    }
}