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
using Main.Scripts.Infrastructure.Services.Samuraism;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Samurais;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Main.Scripts.Infrastructure.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly BlocksConfig _blocksConfig;

        public BlockFactory(ServiceContainer serviceContainer, BlocksConfig blocksConfig)
        {
            _serviceContainer = serviceContainer;
            _blocksConfig = blocksConfig;
        }

        public BlockPiece CreateBlockPiece(Vector2 position)
        {
            BlockInfo blockPieceInfo = _blocksConfig.BlockPiece;
            BlockPiece block = Object.Instantiate(blockPieceInfo.BlockPrefab, position, Quaternion.identity);
            block.Construct(_serviceContainer.Get<IBlockContainerService>(), _serviceContainer.Get<ITimeProvider>(), 0f);
            block.BoundsChecker.Construct(_serviceContainer.Get<LivingZone>(), _serviceContainer.Get<IHealthService>(), false);
            block.BlockAnimation.Construct(_serviceContainer.Get<ITimeProvider>());
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(block);
            return block;
        }

        public Block CreateBlock(Vector2 position, float invulnerabilityDuration)
        {
            Block block = CreateBasicBlock<Block>(out int randomIndex, position, _blocksConfig.Block, true, invulnerabilityDuration);

            block.BlockSlicer.Construct(this, 
                _serviceContainer.Get<ILabelFactory>(), 
                _serviceContainer.Get<ISliceEffectFactory>(), 
                _serviceContainer.Get<IScoreService>(), 
                _serviceContainer.Get<IComboService>(), 
                randomIndex);
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(block);
            return block;
        }
        
        public Bomb CreateBomb(Vector2 position)
        {
            Bomb bomb = CreateBasicBlock<Bomb>(out int randomIndex, position, _blocksConfig.BombConfig.BlockInfo, false, 0f);
            
            bomb.BombSlicer.Construct(
                _serviceContainer.Get<ILabelFactory>(), 
                _serviceContainer.Get<ISliceEffectFactory>(), 
                _serviceContainer.Get<IHealthService>(),
                _serviceContainer.Get<IExplosionService>(),
                randomIndex);
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(bomb);
            
            return bomb;
        }

        public BonusLife CreateBonusLife(Vector2 position)
        {
            BonusLife bonusLife = CreateBasicBlock<BonusLife>(out int randomIndex, position, _blocksConfig.BonusLifeConfig.BlockInfo, false, 0f);
            
            bonusLife.BonusLifeSlicer.Construct(
                _serviceContainer.Get<ILabelFactory>(), 
                _serviceContainer.Get<ISliceEffectFactory>(), 
                _serviceContainer.Get<IHealthService>(),
                randomIndex);
           
            _serviceContainer.Get<IBlockContainerService>().AddBlock(bonusLife);
            
            return bonusLife;
        }
        
        public BlockBag CreateBlockBag(Vector2 position)
        {
            BlockBag blockBag = CreateBasicBlock<BlockBag>(out int randomIndex, position, _blocksConfig.BlockBagConfig.BlockInfo, false, 0f);
            
            blockBag.BlockBagSlicer.Construct(
                this,
                _blocksConfig.BlockBagConfig, 
                _serviceContainer.Get<ISliceEffectFactory>(), 
                randomIndex);
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(blockBag);
            
            return blockBag;
        }
        
        public Freeze CreateFreeze(Vector2 position)
        {
            Freeze freeze = CreateBasicBlock<Freeze>(out int randomIndex, position, _blocksConfig.FreezeConfig.BlockInfo, false, 0f);
            
            freeze.FreezeSlicer.Construct(
                _serviceContainer.Get<IFreezeService>(),
                _serviceContainer.Get<ISliceEffectFactory>(), 
                randomIndex);
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(freeze);
            
            return freeze;
        }
        
        public Magnet CreateMagnet(Vector2 position)
        {
            Magnet magnet = CreateBasicBlock<Magnet>(out int randomIndex, position, _blocksConfig.MagnetConfig.BlockInfo, false, 0f);
            
            magnet.MagnetSlicer.Construct(
                _serviceContainer.Get<ISliceEffectFactory>(),
                _serviceContainer.Get<IMagnetService>(),
                randomIndex);
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(magnet);
            
            return magnet;
        }
        
        public Brick CreateBrick(Vector2 position)
        {
            Brick brick = CreateBasicBlock<Brick>(out int randomIndex, position, _blocksConfig.BrickConfig.BlockInfo, false, 0f);
            
            brick.BrickSlicer.Construct(
                _serviceContainer.Get<IBrickService>(),
                _serviceContainer.Get<ISliceEffectFactory>(), 
                randomIndex);
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(brick);
            
            return brick;
        }
        
        public Samurai CreateSamurai(Vector2 position)
        {
            Samurai samurai = CreateBasicBlock<Samurai>(out int randomIndex, position, _blocksConfig.SamuraiConfig.BlockInfo, false, 0f);
            
            samurai.SamuraiSlicer.Construct(
                _serviceContainer.Get<ISamuraiService>(),
                _serviceContainer.Get<ISliceEffectFactory>(),
                randomIndex);
            
            _serviceContainer.Get<IBlockContainerService>().AddBlock(samurai);
            
            return samurai;
        }

        public void Cleanup()
        {
        }

        private T CreateBasicBlock<T>(out int randomIndex, Vector2 position, BlockInfo blockInfo, bool healthAffect, float invulnerabilityDuration) where T : BlockPiece
        {
            T basicBlock = (T)Object.Instantiate(blockInfo.BlockPrefab, position, Quaternion.identity);
            
            randomIndex = Random.Range(0, blockInfo.VisualSprites.Length);

            basicBlock.Construct(_serviceContainer.Get<IBlockContainerService>(), _serviceContainer.Get<ITimeProvider>(), invulnerabilityDuration);
            basicBlock.SpriteRenderer.sprite = blockInfo.VisualSprites[randomIndex].BlockSprite;
            basicBlock.BoundsChecker.Construct(_serviceContainer.Get<LivingZone>(), _serviceContainer.Get<IHealthService>(), healthAffect);
            basicBlock.BlockAnimation.Construct(_serviceContainer.Get<ITimeProvider>());
            
            return basicBlock;
        }
    }
}