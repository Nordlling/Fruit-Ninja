using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Configs/Score")]
    public class ScoreConfig : ScriptableObject
    {
        public int BasicScoreValue = 50;
        
        [Range(0, 10)] 
        public int ComboMultiplier;

        public float MaxIntervalForCombo;

        public int MaxComboCount;

    }
}