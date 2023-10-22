using Main.Scripts.Logic.Spawn;
using Object = UnityEngine.Object;

namespace Main.Scripts.Infrastructure.Factory
{
    public class SpawnFactory : ISpawnFactory
    {
        private readonly SpawnArea _spawnAreaPrefab;

        public SpawnFactory(SpawnArea spawnAreaPrefab)
        {
            _spawnAreaPrefab = spawnAreaPrefab;
        }

        public SpawnArea CreateSpawnArea()
        {
            SpawnArea spawnArea = Object.Instantiate(_spawnAreaPrefab);
            return spawnArea;
        }

        public void Cleanup()
        {
        }
    }
}