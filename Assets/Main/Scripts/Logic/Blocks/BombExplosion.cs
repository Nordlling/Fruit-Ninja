using Main.Scripts.Infrastructure.Services.Explosion;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BombExplosion : MonoBehaviour
    {
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        
        private IExplosionService _explosionService;

        public void Construct(IExplosionService explosionService)
        {
            _explosionService = explosionService;
        }
        
        public void Explode()
        {
            _explosionService.Explode(transform.position, _explosionForce, _explosionRadius);
        }
    }
}