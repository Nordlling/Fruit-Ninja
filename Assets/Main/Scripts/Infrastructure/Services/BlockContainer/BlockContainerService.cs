using System.Collections.Generic;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;

namespace Main.Scripts.Infrastructure.Services.BlockContainer
{
    public class BlockContainerService : IBlockContainerService, ILoseable
    {
        public List<BlockPiece> AllBlocks { get; private set; } = new();
        public List<Block> Blocks { get; private set; } = new();
        public List<Bomb> Bombs { get; private set; } = new();
        public List<BonusLife> BonusLifes { get; private set; } = new();

        private readonly IGameplayStateMachine _gameplayStateMachine;

        private bool _lose;

        public BlockContainerService(IGameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void AddBlock(Block blockCollider)
        {
            Blocks.Add(blockCollider);
            AllBlocks.Add(blockCollider);
        }

        public void RemoveBlock(Block blockCollider)
        {
            Blocks.Remove(blockCollider);
            AllBlocks.Remove(blockCollider);
            CheckBocksFell();
        }

        public void AddBomb(Bomb bombCollider)
        {
            Bombs.Add(bombCollider);
            AllBlocks.Add(bombCollider);
        }

        public void RemoveBomb(Bomb bombCollider)
        {
            Bombs.Remove(bombCollider);
            AllBlocks.Remove(bombCollider);
            CheckBocksFell();
        }
        
        public void AddBonusLife(BonusLife bonusLifeCollider)
        {
            BonusLifes.Add(bonusLifeCollider);
            AllBlocks.Add(bonusLifeCollider);
        }

        public void RemoveBonusLife(BonusLife bonusLifeCollider)
        {
            BonusLifes.Remove(bonusLifeCollider);
            AllBlocks.Remove(bonusLifeCollider);
            CheckBocksFell();
        }

        public void Lose()
        {
            _lose = true;
        }
        
        private void CheckBocksFell()
        {
            if (_lose && AllBlocks.Count == 0)
            {
                _gameplayStateMachine.Enter<GameOverState>();
                _lose = false;
            }
        }
    }
}