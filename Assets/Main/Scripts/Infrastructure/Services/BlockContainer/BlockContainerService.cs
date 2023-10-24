using System;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;

namespace Main.Scripts.Infrastructure.Services.BlockContainer
{
    public class BlockContainerService : IBlockContainerService, ILoseable
    {
        public List<BlockPiece> AllBlocks { get; private set; } = new();
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
                { typeof(BlockBag), new List<BlockPiece>() }
            };
        }
        
        public void AddBlock<T>(T blockCollider) where T : BlockPiece
        {
            if (BlockTypes.TryGetValue(typeof(T), out List<BlockPiece> blockList))
            {
                blockList.Add(blockCollider);
                AllBlocks.Add(blockCollider);
                BlocksCount = AllBlocks.Count;
            }
            
        }

        public void RemoveBlock<T>(T blockCollider) where T: BlockPiece
        {
            if (BlockTypes.TryGetValue(typeof(T), out List<BlockPiece> blockList))
            {
                blockList.Remove(blockCollider);
                AllBlocks.Remove(blockCollider);
                BlocksCount = AllBlocks.Count;
                CheckBocksFell();
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