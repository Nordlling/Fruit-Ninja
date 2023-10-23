using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.BonusLifes
{
    public class BonusLife : BlockPiece, ISliceable
    {
        public BonusLifeSlicer BonusLifeSlicer => _bonusLifeSlicer;
        public HealthAdder HealthAdder => _healthAdder;
        
        [SerializeField] private BonusLifeSlicer _bonusLifeSlicer;
        [SerializeField] private HealthAdder _healthAdder;
        
        private IBlockContainerService _blockContainerService;
        
        public void Construct(IBlockContainerService blockContainerService, ITimeProvider timeProvider)
        {
            _blockContainerService = blockContainerService;
            TimeProvider = timeProvider;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _bonusLifeSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBonusLife(this);
        }
    }
}