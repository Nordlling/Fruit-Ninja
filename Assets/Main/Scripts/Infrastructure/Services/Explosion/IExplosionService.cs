using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Explosion
{
    public interface IExplosionService : IService
    {
        void Explode(Vector2 explosionPosition);
    }
}