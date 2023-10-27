using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    [CreateAssetMenu(fileName = "BombConfig", menuName = "Configs/Boosters/Bomb")]
    public class BombConfig : BoosterConfig
    {
        [Header("Booster Settings")]
        
        public float ExplosionForce;
        public float ExplosionRadius;
    }
}