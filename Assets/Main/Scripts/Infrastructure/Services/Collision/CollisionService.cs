using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Swipe;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class CollisionService : MonoBehaviour, ICollisionService
    {
        private ISwiper _swiper;
        
        private readonly List<Block> _blocks = new();
        private bool _stop;

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

        public void WaitFallBlocks(Action onBlocksFell)
        {
            _stop = true;
            StartCoroutine(StartWaitFallBlocks(onBlocksFell));
        }

        private IEnumerator StartWaitFallBlocks(Action onBlocksFell)
        {
            while (_blocks.Count > 0)
            {
                yield return null;
            }
            
            onBlocksFell?.Invoke();
            _stop = false;
        }

        private void Update()
        {
            if (_stop)
            {
                return;
            }
            
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