using Main.Scripts.Constants;
using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    [CreateAssetMenu(fileName = "MagnetConfig", menuName = "Configs/Boosters/Magnet")]
    public class MagnetConfig : BoosterConfig
    {
        [Header("Booster Settings")]
        [Range(0f, 20f)]
        public float Duration;
        public float AttractionForce;
        public float AttractionRadius;
        public float SingularityRadius;
        
        public BlockType[] TypesToMagnet;
        
        [Header("Magnet Area Effect")]
        public MagnetAreaEffect MagnetAreaEffectPrefab;
    }
}