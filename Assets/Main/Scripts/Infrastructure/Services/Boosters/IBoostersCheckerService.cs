using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Logic.Spawn;

namespace Main.Scripts.Infrastructure.Services.Boosters
{
    public interface IBoostersCheckerService : IService
    {
        void CalculateBlockMaxCounter(int packBlockCount);
        bool TrySpawnBooster(SpawnArea spawnArea, BoosterInfo boosterInfo);
    }
}