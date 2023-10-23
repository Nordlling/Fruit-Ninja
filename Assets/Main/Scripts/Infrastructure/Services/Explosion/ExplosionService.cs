using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Explosion
{
    public class ExplosionService : IExplosionService
    {
        private readonly IBlockContainerService _blockContainerService;

        public ExplosionService(IBlockContainerService blockContainerService)
        {
            _blockContainerService = blockContainerService;
        }

        public void Explode(Vector2 explosionPosition, float explosionForce, float explosionRadius)
        {
            foreach (BlockPiece block in _blockContainerService.AllBlocks)
            {
                float distance = Vector2.Distance(block.transform.position, explosionPosition);
                
                if (distance >= explosionRadius || block.transform.position.Equals(explosionPosition))
                {
                    continue;
                }

                float force = explosionForce * ((explosionRadius - distance) / explosionRadius);
                Vector2 explosionDirection = ((Vector2)block.transform.position - explosionPosition).normalized;
                Vector2 forcedExplosionDirection = explosionDirection * force;
                block.BlockMovement.AddExplodeForce(forcedExplosionDirection);
            }
        }
        
    }
}