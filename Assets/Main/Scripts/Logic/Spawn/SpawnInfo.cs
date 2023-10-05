using System;
using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    [Serializable]
    public class SpawnInfo
    {
        public SpawnArea SpawnArea { get; set; }
        
        [Range(0f, 100f)]
        public float ProbabilityWeight;
        public SpawnerAreaInfo spawnerAreaInfo;
    }
}