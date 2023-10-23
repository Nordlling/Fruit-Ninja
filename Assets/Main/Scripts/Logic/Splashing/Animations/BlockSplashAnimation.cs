using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing.Animations
{
    public class BlockSplashAnimation : SplashAnimation
    {
        
        private Sequence _sequence;

        private void Update()
        {
            _sequence.timeScale = _timeProvider.GetTimeScale();
        }
        
        public override void PlayAnimation()
        {
            transform.localScale = _initialScale;
            
            _sequence = DOTween.Sequence()
                .AppendInterval(_timeBeforeAnimation)
                .Append(transform.DOMove((Vector2)transform.position + _animationMoveOffset, _animationDuration))
                .Join(transform.DOScale((Vector2)transform.localScale + _animationStretchOffset, _animationDuration))
                .Join(_spriteRenderer.material.DOFade(0f, _animationDuration).OnComplete(() => Destroy(gameObject)))
                .Play();
        }
    }
}