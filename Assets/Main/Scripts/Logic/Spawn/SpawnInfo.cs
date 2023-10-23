using System;

namespace Main.Scripts.Logic.Spawn
{
    [Serializable]
    public class SpawnInfo
    {
        public SpawnArea SpawnArea { get; set; }
        
        public float ProbabilityWeight;
        public SpawnerAreaInfo spawnerAreaInfo;
    }
}