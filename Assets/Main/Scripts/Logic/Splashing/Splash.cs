using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing
{
    public class Splash : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        [SerializeField] private float _timeBeforeAnimation;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _animationMoveOffset;
        [SerializeField] private float _animationStretchOffset;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _splashEffect;

        private void Start()
        {
            PlaySplashEffect();
            Invoke(nameof(PlayAnimation), _timeBeforeAnimation);
        }

        private void PlayAnimation()
        {
            transform.DOMoveY(transform.position.y - _animationMoveOffset, _animationDuration);
            transform.DOScaleY(transform.localScale.y + _animationStretchOffset, _animationDuration);
            _spriteRenderer.material.DOFade(0f, _animationDuration).OnComplete(() => Destroy(gameObject));
        }

        private void PlaySplashEffect()
        {
            SetColorToSplashEffect();
            _splashEffect.Play();
        }

        private void SetColorToSplashEffect()
        {
            Texture2D texture = _spriteRenderer.sprite.texture;
            int centerX = texture.width / 2;
            int centerY = texture.height / 2;
            Color centralPixelColor = texture.GetPixel(centerX, centerY);

            ParticleSystem.MainModule splashEffectMain = _splashEffect.main;
            splashEffectMain.startColor = new ParticleSystem.MinMaxGradient(centralPixelColor);
        }
    }
}