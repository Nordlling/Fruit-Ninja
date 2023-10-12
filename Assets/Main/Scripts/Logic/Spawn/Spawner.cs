using System.Linq;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Utils.RandomUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public SpawnInfo[] SpawnAreas => _spawnAreas;
        
        [SerializeField] private Block _blockPrefab;
        [SerializeField] private SpawnArea _spawnerAreaPrefab;

        [SerializeField] private float _minInterval;
        [SerializeField] private float _maxInterval;
        [SerializeField] private SpawnInfo[] _spawnAreas;

        private IDifficultyService _difficultyService;
        private IGameFactory _gameFactory;
        private float _leftTime;
        private float[] _spawnWeights;

        public void Construct(IDifficultyService difficultyService, IGameFactory gameFactory)
        {
            _difficultyService = difficultyService;
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            _leftTime = Random.Range(_minInterval, _maxInterval);
            CreateSpawnAreas();
            _spawnWeights = _spawnAreas.Select(x => x.ProbabilityWeight).ToArray();
        }

        private void Update()
        {
            if (_leftTime > 0f)
            {
                _leftTime -= Time.deltaTime;
                return;
            }

            int randomIndex = _spawnWeights.GetRandomWeightedIndex();
            if (randomIndex == -1)
            {
                Debug.LogError("Can't find weighted random index");
                randomIndex = Random.Range(0, _spawnWeights.Length);
            }
            
            _difficultyService.IncreaseDifficulty();
            
            _spawnAreas[randomIndex].SpawnArea.SpawnPack(_difficultyService.GetDifficultyLevel());
        
            _leftTime = Random.Range(_minInterval, _maxInterval) + _difficultyService.GetDifficultyLevel().BlockCount * _difficultyService.GetDifficultyLevel().Frequency;
        }

        private void CreateSpawnAreas()
        {
            foreach (SpawnInfo spawnAreaInfo in _spawnAreas)
            {
                SpawnArea spawnArea = Instantiate(_spawnerAreaPrefab);
                spawnAreaInfo.spawnerAreaInfo._blockPrefab = _blockPrefab;
                spawnArea.Construct(spawnAreaInfo.spawnerAreaInfo, _gameFactory);
                spawnAreaInfo.SpawnArea = spawnArea;
            }
        }
    }
}