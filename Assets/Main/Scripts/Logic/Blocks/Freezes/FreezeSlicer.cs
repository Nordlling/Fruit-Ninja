using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Freezing;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Freezes
{
    public class FreezeSlicer : MonoBehaviour, ISlicer
    {
        private IFreezeService _freezeService;
        private ISliceEffectFactory _sliceEffectFactory;
        private int _visualIndex;

        public void Construct
            (
                IFreezeService freezeService,
                ISliceEffectFactory sliceEffectFactory,
                int visualIndex
            )
        {
            _freezeService = freezeService;
            _sliceEffectFactory = sliceEffectFactory;
            _visualIndex = visualIndex;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            SpawnSplash();
            _freezeService.Freeze();
            Destroy(gameObject);
        }

        private void SpawnSplash()
        {
            _sliceEffectFactory.CreateFreezeSplash(transform.position, _visualIndex);
        }
    }
}