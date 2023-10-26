using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Magnets
{
    public class Magnet : BlockPiece, ISliceable
    {
        public MagnetSlicer MagnetSlicer => _magnetSlicer;
        
        [SerializeField] private MagnetSlicer _magnetSlicer;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _magnetSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}