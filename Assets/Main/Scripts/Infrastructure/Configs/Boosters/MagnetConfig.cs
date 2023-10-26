using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    [CreateAssetMenu(fileName = "MagnetConfig", menuName = "Configs/Magnet")]
    public class MagnetConfig : BoosterConfig
    {
        [Header("Booster Settings")]
        [Range(0f, 20f)]
        public float Duration;
        public float AttractionForce;
        public float AttractionRadius;
        public float StrongRadius;
        public float SingularityRadius;
        
        [Header("Magnet Area Effect")]
        public MagnetAreaEffect MagnetAreaEffectPrefab;
    }
}