using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Samurais
{
    public class Samurai : BlockPiece, ISliceable
    {
        public SamuraiSlicer SamuraiSlicer => _samuraiSlicer;
        
        [SerializeField] private SamuraiSlicer _samuraiSlicer;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _samuraiSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}