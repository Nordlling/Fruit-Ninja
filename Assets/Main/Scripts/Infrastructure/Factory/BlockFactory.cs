using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Explosion;
using Main.Scripts.Infrastructure.Services.Freezing;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Freezes;
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
        private readonly IFreezeService _freezeService;
        private readonly BlocksConfig _blocksConfig;

        public BlockFactory(IBlockContainerService blockContainerService,
            IExplosionService explosionService,
            LivingZone livingZone,
            IHealthService healthService,
            IScoreService scoreService,
            IComboService comboService,
            ITimeProvider timeProvider,
            ILabelFactory labelFactory,
            ISliceEffectFactory sliceEffectFactory,
            IFreezeService freezeService,
            BlocksConfig blocksConfig)
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
            _freezeService = freezeService;
            _blocksConfig = blocksConfig;
        }

        public BlockPiece CreateBlockPiece(Vector2 position)
        {
            BlockInfo blockPieceInfo = _blocksConfig.BlockPiece;
            BlockPiece block = Object.Instantiate(blockPieceInfo.BlockPrefab, position, Quaternion.identity);
            block.Construct(_timeProvider, 0f);
            block.BoundsChecker.Construct(_livingZone, _healthService, false);
            block.BlockAnimation.Construct(_timeProvider);
            return block;
        }

        public Block CreateBlock(Vector2 position, float invulnerabilityDuration)
        {
            Block block = CreateBasicBlock<Block>(out int randomIndex, position, _blocksConfig.Block, true, invulnerabilityDuration);

            block.BlockSlicer.Construct(this, _labelFactory, _sliceEffectFactory, _scoreService, _comboService, randomIndex);
            _blockContainerService.AddBlock(block);
            return block;
        }
        
        public Bomb CreateBomb(Vector2 position)
        {
            Bomb bomb = CreateBasicBlock<Bomb>(out int randomIndex, position, _blocksConfig.BombConfig.BlockInfo, false, 0f);
            
            bomb.BombExplosion.Construct(_explosionService);
            bomb.BombSlicer.Construct(
                _labelFactory, 
                _sliceEffectFactory, 
                _healthService,
                bomb.BombExplosion,
                randomIndex);
            
            _blockContainerService.AddBlock(bomb);
            
            return bomb;
        }

        public BonusLife CreateBonusLife(Vector2 position)
        {
            BonusLife bonusLife = CreateBasicBlock<BonusLife>(out int randomIndex, position, _blocksConfig.BonusLifeConfig.BlockInfo, false, 0f);
            
            bonusLife.BonusLifeSlicer.Construct(
                _labelFactory, 
                _sliceEffectFactory, 
                _healthService,
                randomIndex);
           
            _blockContainerService.AddBlock(bonusLife);
            
            return bonusLife;
        }
        
        public BlockBag CreateBlockBag(Vector2 position)
        {
            BlockBag blockBag = CreateBasicBlock<BlockBag>(out int randomIndex, position, _blocksConfig.BlockBagConfig.BlockInfo, false, 0f);
            
            blockBag.BlockBagSlicer.Construct(
                this,
                _blocksConfig.BlockBagConfig, 
                _sliceEffectFactory, 
                randomIndex);
            
            _blockContainerService.AddBlock(blockBag);
            
            return blockBag;
        }
        
        public Freeze CreateFreeze(Vector2 position)
        {
            Freeze freeze = CreateBasicBlock<Freeze>(out int randomIndex, position, _blocksConfig.FreezeConfig.BlockInfo, false, 0f);
            
            freeze.FreezeSlicer.Construct(
                _freezeService,
                _sliceEffectFactory, 
                randomIndex);
            
            _blockContainerService.AddBlock(freeze);
            
            return freeze;
        }

        public void Cleanup()
        {
        }

        private T CreateBasicBlock<T>(out int randomIndex, Vector2 position, BlockInfo blockInfo, bool healthAffect, float invulnerabilityDuration) where T : BlockPiece
        {
            T basicBlock = (T)Object.Instantiate(blockInfo.BlockPrefab, position, Quaternion.identity);
            
            randomIndex = Random.Range(0, blockInfo.VisualSprites.Length);

            basicBlock.Construct(_blockContainerService, _timeProvider, invulnerabilityDuration);
            basicBlock.SpriteRenderer.sprite = blockInfo.VisualSprites[randomIndex].BlockSprite;
            basicBlock.BoundsChecker.Construct(_livingZone, _healthService, healthAffect);
            basicBlock.BlockAnimation.Construct(_timeProvider);
            
            return basicBlock;
        }
    }
}