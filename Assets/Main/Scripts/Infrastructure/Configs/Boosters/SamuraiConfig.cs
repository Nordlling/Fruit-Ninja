using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    [CreateAssetMenu(fileName = "SamuraiConfig", menuName = "Configs/Samurai")]
    public class SamuraiConfig : BoosterConfig
    {
        [Header("Booster Settings")]
        
        [Range(0f, 20f)]
        public float Duration;
        public int BlockCountMultiplier;
        public float PackFrequencyMultiplier;
        public float BlockFrequencyMultiplier;
    }
}