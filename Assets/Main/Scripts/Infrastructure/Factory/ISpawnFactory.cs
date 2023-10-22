using Main.Scripts.Logic.Spawn;

namespace Main.Scripts.Infrastructure.Factory
{
    public interface ISpawnFactory
    {
        SpawnArea CreateSpawnArea();
        void Cleanup();
    }
}