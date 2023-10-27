using System;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBags;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Samurais;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Infrastructure.Services.Mimicring
{
    public class MimicService : IMimicService
    {
        private readonly IMimickingBlockFactory _mimickingBlockFactory;
        private readonly MimicConfig _mimicConfig;

        private readonly List<BlockPiece> _mimicSwitches;

        public MimicService(IMimickingBlockFactory mimickingBlockFactory, MimicConfig mimicConfig)
        {
            _mimickingBlockFactory = mimickingBlockFactory;
            _mimicConfig = mimicConfig;

            _mimicSwitches = new List<BlockPiece>
            {
                _mimicConfig.BlockPrefab,
                _mimicConfig.BombPrefab,
                _mimicConfig.BonusLifePrefab,
                _mimicConfig.BlockBagPrefab,
                _mimicConfig.FreezePrefab,
                _mimicConfig.MagnetPrefab,
                _mimicConfig.BrickPrefab,
                _mimicConfig.SamuraiPrefab
            };

        }

        public BlockPiece GetRandomMimicBlock(Type blockType, Vector2 position)
        {
            BlockPiece blockPrefab = _mimicSwitches[Random.Range(0, _mimicSwitches.Count)];

            if (_mimicSwitches.Count == 1)
            {
                return CreateMimickingBlock(blockPrefab, position);
            }
            
            while (blockType == blockPrefab.GetType())
            {
                blockPrefab = _mimicSwitches[Random.Range(0, _mimicSwitches.Count)];
            }

            return CreateMimickingBlock(blockPrefab, position);
        }
        
        private BlockPiece CreateMimickingBlock(BlockPiece blockPrefab, Vector2 position)
        {
            return blockPrefab switch
            {
                Block =>  _mimickingBlockFactory.CreateMimickingBlock(position),
                Bomb => _mimickingBlockFactory.CreateMimickingBomb(position),
                BonusLife => _mimickingBlockFactory.CreateMimickingBonusLife(position),
                BlockBag => _mimickingBlockFactory.CreateMimickingBlockBag(position),
                Freeze => _mimickingBlockFactory.CreateMimickingFreeze(position),
                Magnet => _mimickingBlockFactory.CreateMimickingMagnet(position),
                Brick => _mimickingBlockFactory.CreateMimickingBrick(position),
                Samurai => _mimickingBlockFactory.CreateMimickingSamurai(position),
                _ => null
            };
        }
    }
}