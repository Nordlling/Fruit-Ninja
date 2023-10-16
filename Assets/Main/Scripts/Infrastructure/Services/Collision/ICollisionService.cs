using System;
using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public interface ICollisionService : IService
    {
        void AddBlock(Block blockCollider);

        void RemoveBlock(Block blockCollider);
        
        void WaitFallBlocks(Action onBlocksFell);
    }
}