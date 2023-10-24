using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Swipe;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class CollisionService : MonoBehaviour, ICollisionService, IPlayable, ILoseable, IPauseable
    {
        private ISwiper _swiper;
        private IBlockContainerService _blockContainerService;
        
        private bool _stop;

        public void Construct(ISwiper swiper, IBlockContainerService blockContainerService)
        {
            _swiper = swiper;
            _blockContainerService = blockContainerService;
        }

        public void Play()
        {
            _stop = false;
        }

        public void Pause()
        {
            _stop = true;
        }

        public void Lose()
        {
            _stop = true;
        }

        private void Update()
        {
            if (_stop)
            {
                return;
            }

            for (int i = 0; i < _blockContainerService.BlocksCount; i++)
            {
                BlockPiece blockPiece = _blockContainerService.AllBlocks[i];
                if (CanSlice(blockPiece))
                {
                    if (blockPiece is ISliceable slicer)
                    {
                        slicer.Slice(_swiper.Position, _swiper.Direction);
                    }
                } 
            }
        }

        private bool CanSlice(BlockPiece blockPiece)
        {
            return blockPiece.InvulnerabilityDuration <= 0 && 
                   _swiper.HasEnoughSpeed() && 
                   blockPiece.BlockCollider.SphereBounds.Contains(_swiper.Position);
        }
    }
}