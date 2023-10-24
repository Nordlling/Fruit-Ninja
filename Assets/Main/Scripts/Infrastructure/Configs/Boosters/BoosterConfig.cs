using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    public class BoosterConfig : ScriptableObject
    {
        [Header("Spawn")] 
        public BoosterSpawnInfo BoosterSpawnInfo;

        [Header("Prefabs")] 
        public BlockInfo BlockInfo;
    }
}