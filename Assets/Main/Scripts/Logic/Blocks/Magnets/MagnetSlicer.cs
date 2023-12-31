using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.Magnetism;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Magnets
{
    public class MagnetSlicer : MonoBehaviour, ISlicer
    {
        
        private ISliceEffectFactory _sliceEffectFactory;
        private IMagnetService _magnetService;
        private int _visualIndex;

        public void Construct
            (
                ISliceEffectFactory sliceEffectFactory,
                IMagnetService magnetService,
                int visualIndex
            )
        {
            _sliceEffectFactory = sliceEffectFactory;
            _magnetService = magnetService;
            _visualIndex = visualIndex;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _magnetService.Attract(transform.position);
            
            SpawnSplash();

            Destroy(gameObject);
        }

        private void SpawnSplash()
        {
            _sliceEffectFactory.CreateMagnetSplash(transform.position, _visualIndex);
            _sliceEffectFactory.CreateMagnetAreaEffect(transform.position);
        }
    }
}