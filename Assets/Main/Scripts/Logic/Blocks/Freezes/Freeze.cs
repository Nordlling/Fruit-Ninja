using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Freezes
{
    public class Freeze : BlockPiece, ISliceable
    {
        public FreezeSlicer FreezeSlicer => _freezeSlicer;
        
        [SerializeField] private FreezeSlicer _freezeSlicer;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _freezeSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}