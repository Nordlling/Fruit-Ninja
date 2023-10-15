using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Splashing;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Main.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly ICollisionService _collisionService;
        private readonly LivingZone _livingZone;
        private readonly IHealthService _healthService;
        private readonly IScoreService _scoreService;
        private readonly BlockConfig _blockConfig;

        public GameFactory(
            ICollisionService collisionService, 
            LivingZone livingZone, 
            IHealthService healthService, 
            IScoreService scoreService,
            BlockConfig blockConfig)
        {
            _collisionService = collisionService;
            _livingZone = livingZone;
            _healthService = healthService;
            _scoreService = scoreService;
            _blockConfig = blockConfig;
        }

        public Block CreateBlock(Block blockPrefab, Vector2 position)
        {
            Block block = Object.Instantiate(blockPrefab, position, Quaternion.identity);
            block.Construct(_collisionService);
            
            int randomIndex = Random.Range(0, _blockConfig.BlockInfos.Length);
            block.SpriteRenderer.sprite = _blockConfig.BlockInfos[randomIndex].BlockSprite;
            
            block.BoundsChecker.Construct(_livingZone, _healthService, true);
            block.Slicer.Construct(this, _scoreService, _blockConfig.BlockInfos[randomIndex].SplashSprite);
            _collisionService.AddBlock(block);
            return block;
        }
        
        public BlockPiece CreateBlockPiece(BlockPiece blockPrefab, Vector2 position)
        {
            BlockPiece block = Object.Instantiate(blockPrefab, position, Quaternion.identity);
            block.BoundsChecker.Construct(_livingZone, _healthService, false);
            return block;
        }
        
        public Splash CreateSplash(Splash splashPrefab, Vector2 position)
        {
            return Object.Instantiate(splashPrefab, position, Quaternion.identity);
        }

        public void Cleanup()
        {
        }
    }
}