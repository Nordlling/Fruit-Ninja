using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BlockBagConfig", menuName = "Configs/BlockBag")]
    public class BlockBagConfig : ScriptableObject
    {
        public float InvulnerabilityDuration;
        
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
        
        public Sprite LeftPart;
        public Sprite RightPart;
        
        public float PartSpeed;
        public float PartAngle;
    }
}