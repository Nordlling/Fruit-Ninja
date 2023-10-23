using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Logic.Label;
using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BombSlicer : MonoBehaviour, ISlicer
    {
        [SerializeField] private ExplosionLabel _explosionLabelPrefab;

        private ILabelFactory _labelFactory;
        private ISliceEffectFactory _sliceEffectFactory;
        private IHealthService _healthService;
        private IComboService _comboService;
        private BombExplosion _bombExplosion;
        private Sprite _splashSprite;

        public void Construct
            (
                ILabelFactory labelFactory,
                ISliceEffectFactory sliceEffectFactory,
                IHealthService healthService,
                BombExplosion bombExplosion,
                Sprite splashSprite
            )
        {
            _labelFactory = labelFactory;
            _sliceEffectFactory = sliceEffectFactory;
            _healthService = healthService;
            _bombExplosion = bombExplosion;
            _splashSprite = splashSprite;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _healthService.DecreaseHealth();
            _bombExplosion.Explode();
            
            SpawnLabel();
            SpawnSplash();

            Destroy(gameObject);
        }

        private void SpawnLabel()
        {
            _labelFactory.CreateExplosionLabel(_explosionLabelPrefab, transform.position);
        }

        private void SpawnSplash()
        {
            Splash splash = _sliceEffectFactory.CreateBombSplash(transform.position);
            splash.SpriteRenderer.sprite = _splashSprite;
        }
    }
}