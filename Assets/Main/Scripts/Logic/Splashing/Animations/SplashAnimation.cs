using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing.Animations
{
    public class SplashAnimation : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected float _timeBeforeAnimation;
        [SerializeField] protected float _animationDuration;
        [SerializeField] protected Vector2 _animationMoveOffset;
        [SerializeField] protected Vector2 _animationStretchOffset;
        [SerializeField] protected Vector2 _initialScale = Vector2.one;

        protected ITimeProvider _timeProvider;
        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public virtual void PlayAnimation()
        {
            
        }
    }
}