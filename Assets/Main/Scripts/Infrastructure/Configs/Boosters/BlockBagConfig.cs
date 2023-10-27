using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    [CreateAssetMenu(fileName = "BlockBagConfig", menuName = "Configs/Boosters/BlockBag")]
    public class BlockBagConfig : BoosterConfig
    {
        public float InvulnerabilityDuration;
        
        [Header("Booster Settings")]
        
        public float MinSpeed;
        public float MaxSpeed;
        
        [Range(-180, 180)]
        public float LeftAngle;
        [Range(-180, 180)]
        public float RightAngle;
        
        [Range(0, 20)]
        public int MinBlockCount;
        [Range(0, 20)]
        public int MaxBlockCount;
        
        [Header("Parts")]
        public Sprite LeftPart;
        public Sprite RightPart;
        
        public float PartSpeed;
        public float PartAngle;
    }
}