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
            
            foreach (Block block in _blockContainerService.Blocks)
            {
                if (_swiper.HasEnoughSpeed() && block.BlockCollider.SphereBounds.Contains(_swiper.Position))
                {
                    block.BlockSlicer.Slice(_swiper.Position, _swiper.Direction);
                }
            }
            
            foreach (Bomb bomb in _blockContainerService.Bombs)
            {
                if (_swiper.HasEnoughSpeed() && bomb.BlockCollider.SphereBounds.Contains(_swiper.Position))
                {
                    bomb.BombSlicer.Slice(_swiper.Position, _swiper.Direction);
                }
            }
        }
    }
}