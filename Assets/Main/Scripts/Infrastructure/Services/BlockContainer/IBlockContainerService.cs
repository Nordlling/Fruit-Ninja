using System;
using System.Collections.Generic;
using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Infrastructure.Services.BlockContainer
{
    public interface IBlockContainerService : IService
    {
        
        List<BlockPiece> AllBlocks { get; }
        List<ISliceable> AllSliceableBlocks { get; }
        int BlocksCount { get; }
        Dictionary<Type, List<BlockPiece>> BlockTypes { get;  }

        void AddBlock<T>(T block) where T : BlockPiece;
        void RemoveBlock<T>(T block) where T : BlockPiece;
    }
}