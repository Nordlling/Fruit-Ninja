using System.Collections.Generic;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;

namespace Main.Scripts.Infrastructure.Services.BlockContainer
{
    public interface IBlockContainerService : IService
    {
        void AddBlock(Block blockCollider);
        void RemoveBlock(Block blockCollider);
        
        void AddBomb(Bomb bombCollider);
        void RemoveBomb(Bomb bombCollider);
        
        void AddBonusLife(BonusLife bonusLifeCollider);
        void RemoveBonusLife(BonusLife bonusLifeCollider);

        List<BlockPiece> AllBlocks { get; }
        List<Block> Blocks { get; }
        List<Bomb> Bombs { get; }
        List<BonusLife> BonusLifes { get; }
    }
}