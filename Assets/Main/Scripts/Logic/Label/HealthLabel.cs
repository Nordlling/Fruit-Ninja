using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.AnimationTargetContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Label
{
    public class HealthLabel : MonoBehaviour
    {
        [SerializeField] private float _timeBeforeAnimation;
        [SerializeField] private float _animationDuration;
        
        private ITimeProvider _timeProvider;
        private Sequence _sequence;
        private IAnimationTargetContainer _animationTargetContainer;

        public void Construct(ITimeProvider timeProvider, IAnimationTargetContainer animationTargetContainer)
        {
            _timeProvider = timeProvider;
            _animationTargetContainer = animationTargetContainer;
        }
        private void Start()
        {
            PlayAnimation();
        }

        private void Update()
        {
            _sequence.timeScale = _timeProvider.Stopped ? 0f : 1f;
        }

        private void PlayAnimation()
        {
            _sequence = DOTween.Sequence()
                .AppendInterval(_timeBeforeAnimation)
                .Append(transform.DOMove(_animationTargetContainer.HealthTarget, _animationDuration))
                .OnKill(() => Destroy(gameObject))
                .Play();
        }
    }
}