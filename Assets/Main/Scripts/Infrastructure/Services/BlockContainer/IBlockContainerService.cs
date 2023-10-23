using System.Collections.Generic;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Swipe;

namespace Main.Scripts.Infrastructure.Services.BlockContainer
{
    public interface IBlockContainerService : IService
    {
        void AddBlock(Block blockCollider);
        void RemoveBlock(Block blockCollider);
        
        void AddBomb(Bomb bombCollider);
        void RemoveBomb(Bomb bombCollider);

        List<BlockPiece> AllBlocks { get; }
        List<Block> Blocks { get; }
        List<Bomb> Bombs { get; }
    }
}