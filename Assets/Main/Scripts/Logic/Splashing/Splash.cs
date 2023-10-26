using System.Security.Cryptography;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Splashing.Animations;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] private SplashAnimation _splashAnimation;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _splashEffect;
        [SerializeField] private bool _setColorFromSprite;
        [SerializeField] private float _lifeTime;

        private ITimeProvider _timeProvider;
        
        private bool _isLifeTime;
        private ParticleSystem.MainModule _splashEffectMain;
        
        public void Construct(ITimeProvider timeProvider, Sprite splashSprite)
        {
            _timeProvider = timeProvider;
            _spriteRenderer.sprite = splashSprite;
            _splashAnimation.Construct(timeProvider);
        }

        private void Start()
        {
            _isLifeTime = _lifeTime != 0;
            _splashEffectMain = _splashEffect.main;

            if (_setColorFromSprite)
            {
                SetColorToSplashEffect();
            }
            // PlaySplashEffect();
            _splashAnimation.PlayAnimation();
        }

        private void Update()
        {
            if (_isLifeTime && _lifeTime <= 0)
            {
                Destroy(gameObject);
            }
            
            _splashEffectMain.simulationSpeed = _timeProvider.Stopped ? 0f : 1f;
            
            if (!_timeProvider.Stopped)
            {
                _lifeTime -= Time.deltaTime;
            }
        }

        private void PlaySplashEffect()
        {
            if (_setColorFromSprite)
            {
                SetColorToSplashEffect();
            }
            _splashEffect.Play();
        }

        private void SetColorToSplashEffect()
        {
            Texture2D texture = _spriteRenderer.sprite.texture;
            int centerX = texture.width / 2;
            int centerY = texture.height / 2;
            Color centralPixelColor = texture.GetPixel(centerX, centerY);

            _splashEffectMain.startColor = new ParticleSystem.MinMaxGradient(centralPixelColor);
        }
    }
}