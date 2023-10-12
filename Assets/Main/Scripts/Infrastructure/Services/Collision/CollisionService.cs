using System.Collections.Generic;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Swipe;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class CollisionService : MonoBehaviour, ICollisionService
    {
        private readonly List<Block> _blocks = new();
        private ISwiper _swiper;

        public void Construct(ISwiper swiper)
        {
            _swiper = swiper;
        }

        public void AddBlock(Block blockCollider)
        {
            _blocks.Add(blockCollider);
        }
        
        public void RemoveBlock(Block blockCollider)
        {
            _blocks.Remove(blockCollider);
        }
        
        private void Update()
        {
            foreach (Block block in _blocks)
            {
                if (_swiper.HasEnoughSpeed() && block.BlockCollider.SphereBounds.Contains(_swiper.Position))
                {
                    block.Slicer.Slice(_swiper.Position);
                }
            }
        }
    }
}