using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Explosion;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Logic.Label;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Bombs
{
    public class BombSlicer : MonoBehaviour, ISlicer
    {
        [SerializeField] private ExplosionLabel _explosionLabelPrefab;

        private ILabelFactory _labelFactory;
        private ISliceEffectFactory _sliceEffectFactory;
        private IHealthService _healthService;
        private IComboService _comboService;
        private IExplosionService _explosionService;
        private Sprite _splashSprite;
        private int _visualIndex;

        public void Construct
            (
                ILabelFactory labelFactory,
                ISliceEffectFactory sliceEffectFactory,
                IHealthService healthService,
                IExplosionService explosionService,
                int visualIndex
            )
        {
            _labelFactory = labelFactory;
            _sliceEffectFactory = sliceEffectFactory;
            _healthService = healthService;
            _explosionService = explosionService;
            _visualIndex = visualIndex;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _healthService.DecreaseHealth();
            _explosionService.Explode(transform.position);
            
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
            _sliceEffectFactory.CreateBombSplash(transform.position, _visualIndex);
        }
    }
}