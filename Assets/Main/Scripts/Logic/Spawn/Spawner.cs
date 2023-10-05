using System.Linq;
using Main.Scripts.Utils;
using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnArea _spawnerAreaPrefab;
        
        [SerializeField] private float _minInterval;
        [SerializeField] private float _maxInterval;
        [SerializeField] private SpawnInfo[] _spawnAreas;
    
        private float _leftTime;
    
        private void Start()
        {
            _leftTime = Random.Range(_minInterval, _maxInterval);
            CreateSpawnAreas();
        }

        private void Update()
        {
            if (_leftTime > 0f)
            {
                _leftTime -= Time.deltaTime;
                return;
            }

            int randomIndex = WeightedRandom.GetRandomWeightedIndex(_spawnAreas.Select(x => x.ProbabilityWeight).ToList());
            _spawnAreas[randomIndex].SpawnArea.SpawnFruit();
        
            _leftTime = Random.Range(_minInterval, _maxInterval);
        }

        private void CreateSpawnAreas()
        {
            foreach (SpawnInfo spawnAreaInfo in _spawnAreas)
            {
                SpawnArea spawnArea = Instantiate(_spawnerAreaPrefab);
                spawnArea.Construct(spawnAreaInfo.spawnerAreaInfo);
                spawnAreaInfo.SpawnArea = spawnArea;
            }
        }
    }
}