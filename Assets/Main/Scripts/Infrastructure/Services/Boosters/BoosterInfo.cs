using System;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Logic.Spawn;

namespace Main.Scripts.Infrastructure.Services.Boosters
{
    public class BoosterInfo
    {
        public BoosterInfo(BoosterSpawnInfo boosterSpawnInfo, Func<BoosterConfig, SpawnArea, Type, bool> spawnAction)
        {
            BoosterSpawnInfo = boosterSpawnInfo;
            SpawnAction = spawnAction;
        }
        
        public int Count;
        public readonly BoosterSpawnInfo BoosterSpawnInfo;
        public readonly Func<BoosterConfig, SpawnArea, Type, bool> SpawnAction;
    }
}