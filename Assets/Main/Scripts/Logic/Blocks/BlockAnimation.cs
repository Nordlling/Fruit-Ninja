using System.Collections.Generic;
using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockAnimation : MonoBehaviour
    {
        [SerializeField] private float _minRotateSpeed;
        [SerializeField] private float _maxRotateSpeed;
        
        [SerializeField] private float _minScaleValue;
        [SerializeField] private float _maxScaleValue;
        [SerializeField] private float _minScaleSpeed;
        [SerializeField] private float _maxScaleSpeed;

        private ITimeProvider _timeProvider;
        
        private readonly List<Tweener> _tweeners = new();

        private void Start()
        {
            FillAnimationList();
            ExecuteAnimations();
        }

        private void Update()
        {
            foreach (Tweener tweener in _tweeners)
            {
                tweener.timeScale = _timeProvider.GetTimeScale();
            }
        }

        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void StopAnimation()
        {
            foreach (Tweener tweener in _tweeners)
            {
                tweener.Pause();
            }
        }

        private void FillAnimationList()
        {
            CreateRotateTweener();
            CreateScaleTweener();
        }

        private void ExecuteAnimations()
        {
            foreach (Tweener tweener in _tweeners)
            {
                if (Random.value > 0.5)
                {
                    tweener.Play();
                }
            }
        }

        private void CreateRotateTweener()
        {
            int randomSign = Random.value < 0.5 ? 1 : -1;
            Vector3 randomRotateValue = new Vector3(0f, 0f, 360f * randomSign);
            float randomRotateDuration = Random.Range(_minRotateSpeed, _maxRotateSpeed);
            
            Tweener tweener = transform.DORotate(randomRotateValue, randomRotateDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            
            _tweeners.Add(tweener);
        }

        private void CreateScaleTweener()
        {
            float randomScaleValue = Random.Range(_minScaleValue, _maxScaleValue);
            float randomScaleDuration = Random.Range(_minScaleSpeed, _maxScaleSpeed);

            Tweener tweener = transform.DOScale(randomScaleValue, randomScaleDuration)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            
            _tweeners.Add(tweener);
        }
    }
}