using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Bomb : BlockPiece
    {
        public BlockCollider BlockCollider => _blockCollider;
        public BombSlicer BombSlicer => bombSlicer;
        public BombExplosion BombExplosion => _bombExplosion;
        
        [SerializeField] private BlockCollider _blockCollider;
        [SerializeField] private BombSlicer bombSlicer;
        [SerializeField] private BombExplosion _bombExplosion;
        
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