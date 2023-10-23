﻿using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Bombs
{
    public class Bomb : BlockPiece, ISliceable
    {
        public BombSlicer BombSlicer => _bombSlicer;
        public BombExplosion BombExplosion => _bombExplosion;
        
        [SerializeField] private BombSlicer _bombSlicer;
        [SerializeField] private BombExplosion _bombExplosion;
        
        private IBlockContainerService _blockContainerService;
        
        public void Construct(IBlockContainerService blockContainerService, ITimeProvider timeProvider)
        {
            _blockContainerService = blockContainerService;
            TimeProvider = timeProvider;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _bombSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBomb(this);
        }
    }
}