using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Samuraism;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Samurais
{
    public class SamuraiSlicer : MonoBehaviour, ISlicer
    {

        private ISamuraiService _samuraiService;
        private ISliceEffectFactory _sliceEffectFactory;
        private int _visualIndex;

        public void Construct
            (
                ISamuraiService samuraiService,
                ISliceEffectFactory sliceEffectFactory,
                int visualIndex
            )
        {
            _sliceEffectFactory = sliceEffectFactory;
            _samuraiService = samuraiService;
            _visualIndex = visualIndex;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _samuraiService.ActivateSamurai();
            
            SpawnSplash();

            Destroy(gameObject);
        }

        private void SpawnSplash()
        {
            _sliceEffectFactory.CreateSamuraiSplash(transform.position, _visualIndex);
        }
    }
}