using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Splashing.Animations;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] private SplashAnimation _splashAnimation;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem[] _splashEffects;
        [SerializeField] private bool _setColorFromSprite;
        [SerializeField] private float _lifeTime;

        private ITimeProvider _timeProvider;
        
        private bool _isLifeTime;
        
        public void Construct(ITimeProvider timeProvider, Sprite splashSprite)
        {
            _timeProvider = timeProvider;
            _spriteRenderer.sprite = splashSprite;
            _splashAnimation.Construct(timeProvider);
        }

        private void Start()
        {
            _isLifeTime = _lifeTime != 0;

            if (_setColorFromSprite)
            {
                SetColorToSplashEffect();
            }
            _splashAnimation.PlayAnimation();
        }

        private void Update()
        {
            if (_isLifeTime && _lifeTime <= 0)
            {
                Destroy(gameObject);
            }
            
            for (int i = 0; i < _splashEffects.Length; i++)
            {
                var splashEffectMain = _splashEffects[i].main;
                splashEffectMain.simulationSpeed = _timeProvider.Stopped ? 0f : 1f;
            }
            
            if (!_timeProvider.Stopped)
            {
                _lifeTime -= Time.deltaTime;
            }
        }

        private void SetColorToSplashEffect()
        {
            Texture2D texture = _spriteRenderer.sprite.texture;
            int centerX = texture.width / 2;
            int centerY = texture.height / 2;
            Color centralPixelColor = texture.GetPixel(centerX, centerY);

            for (int i = 0; i < _splashEffects.Length; i++)
            {
                var splashEffectMain = _splashEffects[i].main;
                splashEffectMain.startColor = new ParticleSystem.MinMaxGradient(centralPixelColor);
            }
        }
    }
}