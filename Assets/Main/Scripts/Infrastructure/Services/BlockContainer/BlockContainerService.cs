using System;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;

namespace Main.Scripts.Infrastructure.Services.BlockContainer
{
    public class BlockContainerService : IBlockContainerService, ILoseable
    {
        public List<BlockPiece> AllBlocks { get; private set; } = new();
        public List<ISliceable> AllSliceableBlocks { get; private set; } = new();
        public int BlocksCount { get; private set; }
        public Dictionary<Type, List<BlockPiece>> BlockTypes { get; private set; }

        private readonly IGameplayStateMachine _gameplayStateMachine;
        private bool _lose;

        public BlockContainerService(IGameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;

            BlockTypes = new Dictionary<Type, List<BlockPiece>>()
            {
                { typeof(BlockPiece), new List<BlockPiece>() },
                { typeof(Block), new List<BlockPiece>() },
                { typeof(Bomb), new List<BlockPiece>() },
                { typeof(BonusLife), new List<BlockPiece>() },
                { typeof(BlockBag), new List<BlockPiece>() },
                { typeof(Freeze), new List<BlockPiece>() },
                { typeof(Magnet), new List<BlockPiece>() },
                { typeof(Brick), new List<BlockPiece>() }
            };
        }

        public void AddBlock<T>(T block) where T : BlockPiece
        {
            if (BlockTypes.TryGetValue(typeof(T), out List<BlockPiece> blockList))
            {
                blockList.Add(block);
                AllBlocks.Add(block);
                BlocksCount = AllBlocks.Count;

                if (block is not ISliceable sliceable)
                {
                    return;
                }
                AllSliceableBlocks.Add(sliceable);
            }
            
        }

        public void RemoveBlock<T>(T block) where T: BlockPiece
        {
            if (BlockTypes.TryGetValue(typeof(T), out List<BlockPiece> blockList))
            {
                blockList.Remove(block);
                AllBlocks.Remove(block);
                BlocksCount = AllBlocks.Count;
                CheckBocksFell();
                
                if (block is not ISliceable sliceable)
                {
                    return;
                }
                AllSliceableBlocks.Remove(sliceable);
            }
        }

        public void Lose()
        {
            _lose = true;
        }
        
        private void CheckBocksFell()
        {
            if (_lose && BlocksCount == 0)
            {
                _gameplayStateMachine.Enter<GameOverState>();
                _lose = false;
            }
        }
    }
}