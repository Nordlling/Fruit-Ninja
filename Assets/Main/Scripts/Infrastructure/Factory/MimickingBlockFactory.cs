using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBags;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Samurais;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
    public class MimickingBlockFactory : BlockFactory, IMimickingBlockFactory
    {
        
        public MimickingBlockFactory(ServiceContainer serviceContainer, BlocksConfig blocksConfig) : base(serviceContainer, blocksConfig)
        {
        }
        
        public Block CreateMimickingBlock(Vector2 position)
        {
            Block block = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.BlockPrefab, _blocksConfig.Block.VisualSprites, position, 0f);
            InitBlockSlicerConstruct(block, randomIndex);
            return block;
        }

        public Bomb CreateMimickingBomb(Vector2 position)
        {
            Bomb bomb = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.BombPrefab, _blocksConfig.BombConfig.BlockInfo.VisualSprites, position, 0f);
            InitBombSlicerConstruct(bomb, randomIndex);
            return bomb;
        }

        public BonusLife CreateMimickingBonusLife(Vector2 position)
        {
            BonusLife bonusLife = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.BonusLifePrefab, _blocksConfig.BonusLifeConfig.BlockInfo.VisualSprites, position, 0f);
            InitBonusLifeSlicerConstruct(bonusLife, randomIndex);
            return bonusLife;
        }

        public BlockBag CreateMimickingBlockBag(Vector2 position)
        {
            BlockBag blockBag = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.BlockBagPrefab, _blocksConfig.BlockBagConfig.BlockInfo.VisualSprites, position, 0f);
            InitBlockBagSlicerConstruct(blockBag, randomIndex);
            return blockBag;
        }

        public Freeze CreateMimickingFreeze(Vector2 position)
        {
            Freeze freeze = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.FreezePrefab, _blocksConfig.FreezeConfig.BlockInfo.VisualSprites, position, 0f);
            InitFreezeSlicerConstruct(freeze, randomIndex);
            return freeze;
        }

        public Magnet CreateMimickingMagnet(Vector2 position)
        {
            Magnet magnet = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.MagnetPrefab, _blocksConfig.MagnetConfig.BlockInfo.VisualSprites, position, 0f);
            InitMagnetSlicerConstruct(magnet, randomIndex);
            return magnet;
        }

        public Brick CreateMimickingBrick(Vector2 position)
        {
            Brick brick = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.BrickPrefab, _blocksConfig.BrickConfig.BlockInfo.VisualSprites, position, 0f);
            InitBrickSlicerConstruct(brick, randomIndex);
            return brick;
        }

        public Samurai CreateMimickingSamurai(Vector2 position)
        {
            Samurai samurai = CreateEmptyBlock(out int randomIndex, _blocksConfig.MimicConfig.SamuraiPrefab, _blocksConfig.SamuraiConfig.BlockInfo.VisualSprites, position, 0f);
            InitSamuraiSlicerConstruct(samurai, randomIndex);
            return samurai;
        }
        
    }
}