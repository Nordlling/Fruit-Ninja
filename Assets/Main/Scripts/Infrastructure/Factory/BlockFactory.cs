using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Explosion;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Main.Scripts.Infrastructure.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private readonly IBlockContainerService _blockContainerService;
        private readonly IExplosionService _explosionService;
        private readonly LivingZone _livingZone;
        private readonly IHealthService _healthService;
        private readonly IScoreService _scoreService;
        private readonly IComboService _comboService;
        private readonly ITimeProvider _timeProvider;
        private readonly ILabelFactory _labelFactory;
        private readonly ISliceEffectFactory _sliceEffectFactory;
        private readonly BlockTypesConfig _blockTypesConfig;

        public BlockFactory(
            IBlockContainerService blockContainerService,
            IExplosionService explosionService,
            LivingZone livingZone,
            IHealthService healthService,
            IScoreService scoreService,
            IComboService comboService,
            ITimeProvider timeProvider,
            ILabelFactory labelFactory,
            ISliceEffectFactory sliceEffectFactory,
            BlockTypesConfig blockTypesConfig)
        {
            _blockContainerService = blockContainerService;
            _explosionService = explosionService;
            _livingZone = livingZone;
            _healthService = healthService;
            _scoreService = scoreService;
            _comboService = comboService;
            _timeProvider = timeProvider;
            _labelFactory = labelFactory;
            _sliceEffectFactory = sliceEffectFactory;
            _blockTypesConfig = blockTypesConfig;
        }

        public BlockPiece CreateBlockPiece(Vector2 position)
        {
            BlockInfo blockPieceInfo = _blockTypesConfig.BlockPiece;
            BlockPiece block = Object.Instantiate(blockPieceInfo.BlockPrefab, position, Quaternion.identity);
            block.Construct(_timeProvider);
            block.BoundsChecker.Construct(_livingZone, _healthService, false);
            block.BlockAnimation.Construct(_timeProvider);
            return block;
        }

        public Block CreateBlock(Vector2 position)
        {
            BlockInfo blockInfo = _blockTypesConfig.Block;
            Block block = (Block)Object.Instantiate(blockInfo.BlockPrefab, position, Quaternion.identity);
            block.Construct(_blockContainerService, _timeProvider);
            
            int randomIndex = Random.Range(0, blockInfo.VisualSprites.Length);
            block.SpriteRenderer.sprite = blockInfo.VisualSprites[randomIndex].BlockSprite;
            block.BoundsChecker.Construct(_livingZone, _healthService, true);
            block.BlockAnimation.Construct(_timeProvider);
            block.BlockSlicer.Construct(this, _labelFactory, _sliceEffectFactory, _scoreService, _comboService, randomIndex);
            _blockContainerService.AddBlock(block);
            return block;
        }
        
        public Bomb CreateBomb(Vector2 position)
        {
            BlockInfo blockInfo = _blockTypesConfig.Bomb;
            Bomb bomb = (Bomb)Object.Instantiate(blockInfo.BlockPrefab, position, Quaternion.identity);
            bomb.Construct(_blockContainerService, _timeProvider);
            
            int randomIndex = Random.Range(0, blockInfo.VisualSprites.Length);
            bomb.SpriteRenderer.sprite = blockInfo.VisualSprites[randomIndex].BlockSprite;
            bomb.BoundsChecker.Construct(_livingZone, _healthService, false);
            bomb.BlockAnimation.Construct(_timeProvider);
            bomb.BombExplosion.Construct(_explosionService);
            bomb.BombSlicer.Construct(
                _labelFactory, 
                _sliceEffectFactory, 
                _healthService,
                bomb.BombExplosion,
                randomIndex);
            _blockContainerService.AddBomb(bomb);
            return bomb;
        }
        
        public BonusLife CreateBonusLife(Vector2 position)
        {
            BlockInfo blockInfo = _blockTypesConfig.BonusLife;
            BonusLife bonusLife = (BonusLife)Object.Instantiate(blockInfo.BlockPrefab, position, Quaternion.identity);
            bonusLife.Construct(_blockContainerService, _timeProvider);
            
            int randomIndex = Random.Range(0, blockInfo.VisualSprites.Length);
            bonusLife.SpriteRenderer.sprite = blockInfo.VisualSprites[randomIndex].BlockSprite;
            bonusLife.BoundsChecker.Construct(_livingZone, _healthService, false);
            bonusLife.BlockAnimation.Construct(_timeProvider);
            bonusLife.BonusLifeSlicer.Construct(
                _labelFactory, 
                _sliceEffectFactory, 
                _healthService,
                randomIndex);
           
            _blockContainerService.AddBonusLife(bonusLife);
            return bonusLife;
        }

        public void Cleanup()
        {
        }
    }
}