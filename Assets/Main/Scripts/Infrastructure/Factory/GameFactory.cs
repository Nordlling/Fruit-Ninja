using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Combo;
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
        private readonly IBlockContainerService _blockContainerService;
        private readonly LivingZone _livingZone;
        private readonly IHealthService _healthService;
        private readonly IScoreService _scoreService;
        private readonly IComboService _comboService;
        private readonly ITimeProvider _timeProvider;
        private readonly ILabelFactory _labelFactory;
        private readonly BlockConfig _blockConfig;

        public GameFactory(
            IBlockContainerService blockContainerService,
            LivingZone livingZone, 
            IHealthService healthService, 
            IScoreService scoreService,
            IComboService comboService,
            ITimeProvider timeProvider,
            ILabelFactory labelFactory,
            BlockConfig blockConfig
        )
        {
            _blockContainerService = blockContainerService;
            _livingZone = livingZone;
            _healthService = healthService;
            _scoreService = scoreService;
            _comboService = comboService;
            _timeProvider = timeProvider;
            _blockConfig = blockConfig;
            _labelFactory = labelFactory;
        }

        public Block CreateBlock(Block blockPrefab, Vector2 position)
        {
            Block block = Object.Instantiate(blockPrefab, position, Quaternion.identity);
            block.Construct(_blockContainerService, _timeProvider);
            
            int randomIndex = Random.Range(0, _blockConfig.BlockInfos.Length);
            block.SpriteRenderer.sprite = _blockConfig.BlockInfos[randomIndex].BlockSprite;
            block.BoundsChecker.Construct(_livingZone, _healthService, true);
            block.BlockAnimation.Construct(_timeProvider);
            block.Slicer.Construct(this, _labelFactory, _scoreService, _comboService, _blockConfig.BlockInfos[randomIndex].SplashSprite);
            _blockContainerService.AddBlock(block);
            return block;
        }
        
        public BlockPiece CreateBlockPiece(BlockPiece blockPrefab, Vector2 position)
        {
            BlockPiece block = Object.Instantiate(blockPrefab, position, Quaternion.identity);
            block.Construct(_timeProvider);
            block.BoundsChecker.Construct(_livingZone, _healthService, false);
            block.BlockAnimation.Construct(_timeProvider);
            return block;
        }
        
        public Splash CreateSplash(Splash splashPrefab, Vector2 position)
        {
            Splash splash = Object.Instantiate(splashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider);
            return splash;
        }

        public void Cleanup()
        {
        }
    }
}