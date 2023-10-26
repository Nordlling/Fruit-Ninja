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
            foreach (BlockPiece block in _blockContainerService.AllBlocks)
            {
                float distance = Vector2.Distance(block.transform.position, explosionPosition);
                
                if (distance >= _bombConfig.ExplosionRadius || block.transform.position.Equals(explosionPosition))
                {
                    continue;
                }

                float force = _bombConfig.ExplosionForce * ((_bombConfig.ExplosionRadius - distance) / _bombConfig.ExplosionRadius);
                Vector2 explosionDirection = ((Vector2)block.transform.position - explosionPosition).normalized;
                Vector2 forcedExplosionDirection = explosionDirection * force;
                block.BlockMovement.AddForceOnce(forcedExplosionDirection);
            }
        }
        
    }
}