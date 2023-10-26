using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Bricking;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Bricks
{
    public class BrickSlicer : MonoBehaviour, ISlicer
    {
        private IBrickService _brickService;
        private ISliceEffectFactory _sliceEffectFactory;
        private int _visualIndex;

        public void Construct(IBrickService brickService, ISliceEffectFactory sliceEffectFactory, int visualIndex)
        {
            _brickService = brickService;
            _sliceEffectFactory = sliceEffectFactory;
            _visualIndex = visualIndex;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            SpawnSplash(swiperPosition);
            _brickService.Brick();
        }

        private void SpawnSplash(Vector2 swiperPosition)
        {
            _sliceEffectFactory.CreateBrickSplash(swiperPosition, _visualIndex);
        }
    }
}