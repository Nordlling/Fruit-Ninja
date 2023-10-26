using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Bricks
{
    public class Brick : BlockPiece, ISliceable
    {
        public BrickSlicer BrickSlicer => _brickSlicer;
        
        [SerializeField] private BrickSlicer _brickSlicer;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _brickSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}