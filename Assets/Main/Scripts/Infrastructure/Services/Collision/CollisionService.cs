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

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            if (!_swiper.HasEnoughSpeed())
            {
                return;
            }
            
            for (int i = 0; i < _blockContainerService.AllSliceableBlocks.Count; i++)
            {
                ISliceable sliceable = _blockContainerService.AllSliceableBlocks[i];
                
                if (CanSlice(sliceable))
                {
                    sliceable.Slice(_swiper.Position, _swiper.Direction);
                }
            }
        }

        private bool CanSlice(ISliceable sliceable)
        {
            return sliceable.InvulnerabilityDuration <= 0 && sliceable.ColliderBounds.Contains(_swiper.Position);
        }
    }
}