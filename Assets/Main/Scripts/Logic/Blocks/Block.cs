using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : BlockPiece, ISliceable
    {
        public BlockSlicer BlockSlicer => _blockSlicer;
        
        [SerializeField] private BlockSlicer _blockSlicer;
        
        private IBlockContainerService _blockContainerService;
        
        public void Construct(IBlockContainerService blockContainerService, ITimeProvider timeProvider)
        {
            _blockContainerService = blockContainerService;
            TimeProvider = timeProvider;
        }

        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _blockSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}