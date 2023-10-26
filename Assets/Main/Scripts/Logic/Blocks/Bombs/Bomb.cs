using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Bombs
{
    public class Bomb : BlockPiece, ISliceable
    {
        public BombSlicer BombSlicer => _bombSlicer;
        
        [SerializeField] private BombSlicer _bombSlicer;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _bombSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}