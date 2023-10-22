using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts.Logic.Blocks
{
    public class Bomb : BlockPiece
    {
        public BlockCollider BlockCollider => _blockCollider;
        public BombSlicer BombSlicer => bombSlicer;
        
        [SerializeField] private BlockCollider _blockCollider;
        [SerializeField] private BombSlicer bombSlicer;
        
        private IBlockContainerService _blockContainerService;
        
        public void Construct(IBlockContainerService blockContainerService, ITimeProvider timeProvider)
        {
            _blockContainerService = blockContainerService;
            TimeProvider = timeProvider;
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBomb(this);
        }
    }
}