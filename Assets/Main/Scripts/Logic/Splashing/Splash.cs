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
        
        private ITimeProvider _timeProvider;
        private ParticleSystem.MainModule _splashEffectMain;

        public void Construct(ITimeProvider timeProvider, Sprite splashSprite)
        {
            _timeProvider = timeProvider;
            _spriteRenderer.sprite = splashSprite;
            _splashAnimation.Construct(timeProvider);
        }

        private void Start()
        {
            _splashEffectMain = _splashEffect.main;

            PlaySplashEffect();
            _splashAnimation.PlayAnimation();
        }

        private void Update()
        {
            _splashEffectMain.simulationSpeed = _timeProvider.GetTimeScale();
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