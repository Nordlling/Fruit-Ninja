using System;
using System.Collections;
using DG.Tweening;
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

        public void Construct(string value)
        {
            _scoreValue.text = value;
        }
        private void Start()
        {
            Invoke(nameof(PlayAnimation), _timeBeforeAnimation);
        }

        private void PlayAnimation()
        {
            transform.DOMoveY(transform.position.y + _animationMoveOffset, _animationDuration).OnComplete(() => Destroy(gameObject));;
            StartCoroutine(StartScoreAnimation(0f));
        }

        private IEnumerator StartScoreAnimation(float to)
        {
            while (Math.Abs(_scoreValue.alpha - to) > float.Epsilon)
            {
                _scoreValue.alpha = Mathf.Lerp(_scoreValue.alpha, to, _animationFadeSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}