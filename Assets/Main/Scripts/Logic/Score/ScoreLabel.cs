using System;
using System.Collections;
using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Logic.Score
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
                .Append(transform.DOMoveY(transform.position.y + _animationMoveOffset, _animationDuration))
                .OnComplete(() => Destroy(gameObject));

            _sequence.Play();
            
            StartCoroutine(StartScoreAnimation(0f));
        }

        private IEnumerator StartScoreAnimation(float to)
        {
            while (Math.Abs(_scoreValue.alpha - to) > float.Epsilon)
            {
                _scoreValue.alpha = Mathf.Lerp(_scoreValue.alpha, to, _animationFadeSpeed * _timeProvider.GetDeltaTime());
                yield return null;
            }
        }
    }
}