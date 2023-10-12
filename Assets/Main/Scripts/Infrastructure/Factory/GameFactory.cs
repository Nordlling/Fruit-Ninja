using System;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Logic;
using Main.Scripts.Logic.Blocks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly ICollisionService _collisionService;
        private readonly LivingZone _livingZone;

        public GameFactory(ICollisionService collisionService, LivingZone livingZone)
        {
            _collisionService = collisionService;
            _livingZone = livingZone;
        }

        public Block CreateBlock(Block blockPrefab, Vector2 position)
        {
            Block block = Object.Instantiate(blockPrefab, position, Quaternion.identity);
            block.Construct(_collisionService);
            block.BoundsChecker.Construct(_livingZone);
            block.Slicer.Construct(this);
            _collisionService.AddBlock(block);
            return block;
        }
        
        public BlockPiece CreateBlockPiece(BlockPiece blockPrefab, Vector2 position)
        {
            BlockPiece block = Object.Instantiate(blockPrefab, position, Quaternion.identity);
            block.BoundsChecker.Construct(_livingZone);
            return block;
        }

        public void Cleanup()
        {
        }
    }
}