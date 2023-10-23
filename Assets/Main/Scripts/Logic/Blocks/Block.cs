﻿using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : BlockPiece
    {
        public BlockCollider BlockCollider => _blockCollider;
        public BlockSlicer BlockSlicer => blockSlicer;
        
        [SerializeField] private BlockCollider _blockCollider;
        [SerializeField] private BlockSlicer blockSlicer;
        
        private IBlockContainerService _blockContainerService;
        
        public void Construct(IBlockContainerService blockContainerService, ITimeProvider timeProvider)
        {
            _blockContainerService = blockContainerService;
            TimeProvider = timeProvider;
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}