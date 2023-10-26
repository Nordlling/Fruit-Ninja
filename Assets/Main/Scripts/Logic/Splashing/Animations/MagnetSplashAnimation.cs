using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing.Animations
{
    public class MagnetSplashAnimation : SplashAnimation
    {
        
        private Sequence _sequence;

        private void Update()
        {
            _sequence.timeScale = _timeProvider.Stopped ? 0f : 1f;
        }
        
        public override void PlayAnimation()
        {
            transform.localScale = _initialScale;
            
            _sequence = DOTween.Sequence()
                .AppendInterval(_timeBeforeAnimation)
                .Append(transform.DOScale((Vector2)transform.localScale + _animationStretchOffset, _animationDuration))
                .Join(transform.DOMove((Vector2)transform.position + _animationMoveOffset, _animationDuration))
                .Append(_spriteRenderer.material.DOFade(0f, _animationDuration)).OnComplete(() => Destroy(gameObject))
                .Play();
        }
    }
}