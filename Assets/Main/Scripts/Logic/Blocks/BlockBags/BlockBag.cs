using UnityEngine;

namespace Main.Scripts.Logic.Blocks.BlockBags
{
    public class BlockBag : BlockPiece, ISliceable
    {
        public BlockBagSlicer BlockBagSlicer => _blockBagSlicer;
        
        [SerializeField] private BlockBagSlicer _blockBagSlicer;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _blockBagSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}