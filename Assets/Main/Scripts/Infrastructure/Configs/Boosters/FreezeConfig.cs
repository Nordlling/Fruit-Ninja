using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    [CreateAssetMenu(fileName = "FreezeConfig", menuName = "Configs/Freeze")]
    public class FreezeConfig : BoosterConfig
    {
        [Range(0f, 1f)]
        public float TimeScale;
        
        [Range(0f, 20f)]
        public float Duration;
        
    }
}