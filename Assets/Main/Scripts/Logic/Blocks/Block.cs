using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : BlockPiece, ISliceable
    {
        public BlockSlicer BlockSlicer => _blockSlicer;
        
        [SerializeField] private BlockSlicer _blockSlicer;

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