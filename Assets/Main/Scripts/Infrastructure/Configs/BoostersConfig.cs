using System;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BoostersConfig", menuName = "Configs/Boosters")]
    public class BoostersConfig : ScriptableObject
    {
        public BoosterInfo[] Boosters;
    }
    
    [Serializable]
    public class BoosterInfo
    {
        public BoosterType BoosterType;
        public BoosterSpawnInfo BoosterSpawnInfo;
    }
    
    [Serializable]
    public class BoosterSpawnInfo
    {
        public float DropoutRate;
        [Range(0, 1)]
        public float MaxFractionInPack;
        [Range(-1, 20)]
        public int MaxNumberInPack;
        [Range(-1, 20)]
        public int MaxNumberOnScreen;
    }
}