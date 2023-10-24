using System.Collections.Generic;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Logic.Spawn;

namespace Main.Scripts.Infrastructure.Services.Boosters
{
    public interface IBoostersCheckerService : IService
    {
        int MaxCountInPack { get; }
        void CalculateBlockMaxCounter(int packBlockCount);
        bool TrySpawnBooster(SpawnArea spawnArea, int index);
        List<BoosterConfig> BoosterConfigs { get; }
    }
}