using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Configs/Difficulty")]
    public class DifficultyConfig : ScriptableObject
    {
        [Range(0, 25)]
        public int LevelsToIncreaseBlockCount;
        [Range(0, 25)]
        public int LevelsToIncreaseFrequency;
        
        [Range(0, 25)]
        public int BlockCountProgression;
        [Range(0, 1)]
        public float FrequencyProgression;
        
        [Range(0, 25)]
        public int InitialBlockCount;
        [Range(0, 10)]
        public float InitialFrequency;

        public int MaxBlockCount;
        public float MinFrequency;
    }
}