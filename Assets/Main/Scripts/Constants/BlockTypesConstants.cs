using System;
using System.Collections.Generic;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBags;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Mimics;
using Main.Scripts.Logic.Blocks.Samurais;

namespace Main.Scripts.Constants
{
    public static class BlockTypesConstants
    {
        public static readonly Dictionary<BlockType, Type> BlockTypes = new()
        {
            { BlockType.BlockPiece, typeof(BlockPiece) },
            { BlockType.Block, typeof(Block) },
            { BlockType.Bomb, typeof(Bomb) },
            { BlockType.BonusLife, typeof(BonusLife) },
            { BlockType.BlockBag, typeof(BlockBag) },
            { BlockType.Freeze, typeof(Freeze) },
            { BlockType.Magnet, typeof(Magnet) },
            { BlockType.Samurai, typeof(Samurai) },
            { BlockType.Brick, typeof(Brick) },
            { BlockType.Mimic, typeof(Mimic) }
        };
    }
    
    public enum BlockType
    {
        BlockPiece,
        Block,
        Bomb,
        BonusLife,
        BlockBag,
        Freeze,
        Magnet,
        Brick,
        Samurai,
        Mimic
    }
}