using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Logic.Label;
using Main.Scripts.Logic.Splashing;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts.Logic.Blocks.BonusLifes
{
    public class BonusLifeSlicer : MonoBehaviour, ISlicer
    {
        [SerializeField] private HealthLabel _labelPrefab;

        private ILabelFactory _labelFactory;
        private ISliceEffectFactory _sliceEffectFactory;
        private IHealthService _healthService;
        private IComboService _comboService;
        private HealthAdder _healthAdder;
        private Sprite _splashSprite;

        public void Construct
            (
                ILabelFactory labelFactory,
                ISliceEffectFactory sliceEffectFactory,
                IHealthService healthService,
                HealthAdder healthAdder,
                Sprite splashSprite
            )
        {
            _labelFactory = labelFactory;
            _sliceEffectFactory = sliceEffectFactory;
            _healthService = healthService;
            _healthAdder = healthAdder;
            _splashSprite = splashSprite;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            if (!_healthService.IsMaxHealth())
            {
                _healthService.IncreaseHealth();
                SpawnLabel();
            }
            
            SpawnSplash();

            Destroy(gameObject);
        }

        private void SpawnLabel()
        {
            _labelFactory.CreateHealthLabel(_labelPrefab, transform.position);
        }

        private void SpawnSplash()
        {
            Splash splash = _sliceEffectFactory.CreateBonusLifeSplash(transform.position);
            splash.SpriteRenderer.sprite = _splashSprite;
        }
    }
}