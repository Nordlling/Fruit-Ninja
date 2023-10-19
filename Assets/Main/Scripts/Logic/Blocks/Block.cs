using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : BlockPiece
    {
        public BlockCollider BlockCollider => _blockCollider;
        public Slicer Slicer => _slicer;
        
        [SerializeField] private BlockCollider _blockCollider;
        [SerializeField] private Slicer _slicer;
        
        private IBlockContainerService _blockContainerService;
        
        public void Construct(IBlockContainerService blockContainerService, ITimeProvider timeProvider)
        {
            _blockContainerService = blockContainerService;
            TimeProvider = timeProvider;
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}