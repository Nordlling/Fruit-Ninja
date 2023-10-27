using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Bricking;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Explosion;
using Main.Scripts.Infrastructure.Services.Freezing;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.Magnetism;
using Main.Scripts.Infrastructure.Services.Mimicring;
using Main.Scripts.Infrastructure.Services.Samuraism;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBags;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Mimics;
using Main.Scripts.Logic.Blocks.Samurais;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Main.Scripts.Infrastructure.Factory
{
    public class BlockFactory : IBlockFactory
    {
        protected readonly ServiceContainer _serviceContainer;
        protected readonly BlocksConfig _blocksConfig;

        public BlockFactory(ServiceContainer serviceContainer, BlocksConfig blocksConfig)
        {
            _serviceContainer = serviceContainer;
            _blocksConfig = blocksConfig;
        }

        public BlockPiece CreateBlockPiece(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.BlockPiece;
            BlockPiece blockPiece = CreateEmptyBlock(out int randomIndex, blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
            
            InitBasicConstructs(blockPiece, false);
            
            return blockPiece;
        }

        public Block CreateBlock(Vector2 position, float invulnerabilityDuration)
        {
            BlockInfo blockInfo = _blocksConfig.Block;
            Block block = CreateEmptyBlock(out int randomIndex, (Block)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, invulnerabilityDuration);
            
            InitBasicConstructs(block, true);
            InitBlockSlicerConstruct(block, randomIndex);
            
            return block;
        }

        public Bomb CreateBomb(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.BombConfig.BlockInfo;
            Bomb bomb = CreateEmptyBlock(out int randomIndex, (Bomb)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
           
            InitBasicConstructs(bomb, false);
            InitBombSlicerConstruct(bomb, randomIndex);
            
            return bomb;
        }

        public BonusLife CreateBonusLife(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.BonusLifeConfig.BlockInfo;
            BonusLife bonusLife = CreateEmptyBlock(out int randomIndex, (BonusLife)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
            
            InitBasicConstructs(bonusLife, false);
            InitBonusLifeSlicerConstruct(bonusLife, randomIndex);
            
            return bonusLife;
        }

        public BlockBag CreateBlockBag(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.BlockBagConfig.BlockInfo;
            BlockBag blockBag = CreateEmptyBlock(out int randomIndex, (BlockBag)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
           
            InitBasicConstructs(blockBag, false);
            InitBlockBagSlicerConstruct(blockBag, randomIndex);
            
            return blockBag;
        }

        public Freeze CreateFreeze(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.FreezeConfig.BlockInfo;
            Freeze freeze = CreateEmptyBlock(out int randomIndex, (Freeze)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
            
            InitBasicConstructs(freeze, false);
            InitFreezeSlicerConstruct(freeze, randomIndex);
            
            return freeze;
        }

        public Magnet CreateMagnet(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.MagnetConfig.BlockInfo;
            Magnet magnet = CreateEmptyBlock(out int randomIndex, (Magnet)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
            
            InitBasicConstructs(magnet, false);
            InitMagnetSlicerConstruct(magnet, randomIndex);
            
            return magnet;
        }

        public Brick CreateBrick(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.BrickConfig.BlockInfo;
            Brick brick = CreateEmptyBlock(out int randomIndex, (Brick)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
            
            InitBasicConstructs(brick, false);
            InitBrickSlicerConstruct(brick, randomIndex);
            
            return brick;
        }

        public Samurai CreateSamurai(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.SamuraiConfig.BlockInfo;
            Samurai samurai = CreateEmptyBlock(out int randomIndex, (Samurai)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
            
            InitBasicConstructs(samurai, false);
            InitSamuraiSlicerConstruct(samurai, randomIndex);
            
            return samurai;
        }

        public Mimic CreateMimic(Vector2 position)
        {
            BlockInfo blockInfo = _blocksConfig.MimicConfig.BlockInfo;
            Mimic mimic = CreateEmptyBlock(out int randomIndex, (Mimic)blockInfo.BlockPrefab, blockInfo.VisualSprites, position, 0f);
            
            InitBasicConstructs(mimic, false);
            InitMimicSwitcherConstruct(mimic);
            
            return mimic;
        }
        
        public void Cleanup()
        {
        }

        
        
        protected T CreateEmptyBlock<T>(out int randomIndex, T blockPrefab, VisualSprites[] visualSprites, Vector2 position, float invulnerabilityDuration) where T : BlockPiece
        {
            randomIndex = Random.Range(0, visualSprites.Length);
            
            T emptyBlock = Object.Instantiate(blockPrefab, position, Quaternion.identity);
            emptyBlock.Construct(_serviceContainer.Get<IBlockContainerService>(), _serviceContainer.Get<ITimeProvider>(), invulnerabilityDuration);
            
            if (visualSprites.Length != 0)
            {
                emptyBlock.SpriteRenderer.sprite = visualSprites[randomIndex].BlockSprite;
            }
            
            AddToBlockContainer(emptyBlock);
            
            return emptyBlock;
        }

        private void AddToBlockContainer<T>(T emptyBlock) where T : BlockPiece
        {
            _serviceContainer.Get<IBlockContainerService>().AddBlock(emptyBlock);
        }
        
        private void InitBasicConstructs(BlockPiece basicBlock, bool healthAffect)
        {
            basicBlock.BoundsChecker.Construct(_serviceContainer.Get<LivingZone>(), _serviceContainer.Get<IHealthService>(), healthAffect);
            basicBlock.BlockAnimation.Construct(_serviceContainer.Get<ITimeProvider>());
        }
        
        protected void InitBlockSlicerConstruct(Block block, int randomIndex)
        {
            block.BlockSlicer.Construct(this,
                _serviceContainer.Get<ILabelFactory>(),
                _serviceContainer.Get<ISliceEffectFactory>(),
                _serviceContainer.Get<IScoreService>(),
                _serviceContainer.Get<IComboService>(),
                randomIndex);
        }

        protected void InitBombSlicerConstruct(Bomb bomb, int randomIndex)
        {
            bomb.BombSlicer.Construct(
                _serviceContainer.Get<ILabelFactory>(),
                _serviceContainer.Get<ISliceEffectFactory>(),
                _serviceContainer.Get<IHealthService>(),
                _serviceContainer.Get<IExplosionService>(),
                randomIndex);
        }

        protected void InitBonusLifeSlicerConstruct(BonusLife bonusLife, int randomIndex)
        {
            bonusLife.BonusLifeSlicer.Construct(
                _serviceContainer.Get<ILabelFactory>(),
                _serviceContainer.Get<ISliceEffectFactory>(),
                _serviceContainer.Get<IHealthService>(),
                randomIndex);
        }

        protected void InitBlockBagSlicerConstruct(BlockBag blockBag, int randomIndex)
        {
            blockBag.BlockBagSlicer.Construct(
                this,
                _blocksConfig.BlockBagConfig,
                _serviceContainer.Get<ISliceEffectFactory>(),
                randomIndex);
        }

        protected void InitFreezeSlicerConstruct(Freeze freeze, int randomIndex)
        {
            freeze.FreezeSlicer.Construct(
                _serviceContainer.Get<IFreezeService>(),
                _serviceContainer.Get<ISliceEffectFactory>(),
                randomIndex);
        }

        protected void InitMagnetSlicerConstruct(Magnet magnet, int randomIndex)
        {
            magnet.MagnetSlicer.Construct(
                _serviceContainer.Get<ISliceEffectFactory>(),
                _serviceContainer.Get<IMagnetService>(),
                randomIndex);
        }

        protected void InitBrickSlicerConstruct(Brick brick, int randomIndex)
        {
            brick.BrickSlicer.Construct(
                _serviceContainer.Get<IBrickService>(),
                _serviceContainer.Get<ISliceEffectFactory>(),
                randomIndex);
        }

        protected void InitSamuraiSlicerConstruct(Samurai samurai, int randomIndex)
        {
            samurai.SamuraiSlicer.Construct(
                _serviceContainer.Get<ISamuraiService>(),
                _serviceContainer.Get<ISliceEffectFactory>(),
                randomIndex);
        }

        private void InitMimicSwitcherConstruct(Mimic mimic)
        {
            mimic.MimicSwitcher.Construct(
                _serviceContainer.Get<IMimicService>(),
                _serviceContainer.Get<ITimeProvider>(),
                _blocksConfig.MimicConfig);
        }
    }
}