using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Explosion
{
    public class ExplosionService : IExplosionService
    {
        private readonly IBlockContainerService _blockContainerService;
        private readonly BombConfig _bombConfig;

        public ExplosionService(IBlockContainerService blockContainerService, BombConfig bombConfig)
        {
            _blockContainerService = blockContainerService;
            _bombConfig = bombConfig;
        }

        public void Explode(Vector2 explosionPosition)
        {
            for (int i = 0; i < _blockContainerService.BlocksCount; i++)
            {
                BlockPiece block = _blockContainerService.AllBlocks[i];
                
                float distance = Vector2.Distance(block.transform.position, explosionPosition);
                if (distance >= _bombConfig.ExplosionRadius || block.transform.position.Equals(explosionPosition))
                {
                    continue;
                }

                float force = _bombConfig.ExplosionForce * ((_bombConfig.ExplosionRadius - distance) / _bombConfig.ExplosionRadius);
                Vector2 explosionDirection = ((Vector2)block.transform.position - explosionPosition).normalized;
                Vector2 forcedExplosionDirection = explosionDirection * force;
                if (block.BlockMovement != null)
                {
                    block.BlockMovement.AddForceOnce(forcedExplosionDirection);
                }
            }
        }
        
    }
}