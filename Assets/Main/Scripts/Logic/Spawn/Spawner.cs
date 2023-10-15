using System;
using System.Collections;
using System.Linq;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Health;
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
        private IHealthService _healthService;
        
        private float _leftTime;
        private float[] _spawnWeights;

        private bool _spawnPackBusy;
        private bool _stop;

        public void Construct(IDifficultyService difficultyService, IGameFactory gameFactory, IHealthService healthService)
        {
            _difficultyService = difficultyService;
            _gameFactory = gameFactory;
            _healthService = healthService;
        }

        private void OnEnable()
        {
            _healthService.OnDied += StopSpawn;
        }
        
        private void OnDisable()
        {
            _healthService.OnDied -= StopSpawn;
        }

        private void StopSpawn()
        {
            _stop = true;
        }
        
        private void StartSpawn()
        {
            _stop = false;
        }

        private void Start()
        {
            _leftTime = Random.Range(_minInterval, _maxInterval);
            CreateSpawnAreas();
            _spawnWeights = _spawnAreas.Select(x => x.ProbabilityWeight).ToArray();
        }

        private void Update()
        {
            if (_spawnPackBusy || _stop)
            {
                return;
            }
            
            if (_leftTime > 0f)
            {
                _leftTime -= Time.deltaTime;
                return;
            }

            _difficultyService.IncreaseDifficulty();
            StartCoroutine(SpawnPack(() => _spawnPackBusy = false));

            _leftTime = Random.Range(_minInterval, _maxInterval);
        }

        private int GenerateRandomIndex()
        {
            int randomIndex = _spawnWeights.GetRandomWeightedIndex();
            if (randomIndex == -1)
            {
                Debug.LogError("Can't find weighted random index");
                randomIndex = Random.Range(0, _spawnWeights.Length);
            }

            return randomIndex;
        }
        
        private IEnumerator SpawnPack(Action onPackSpawned = null)
        {
            _spawnPackBusy = true;
            for (int i = 0; i < _difficultyService.GetDifficultyLevel().BlockCount; i++)
            {
                int randomIndex = GenerateRandomIndex();
                _spawnAreas[randomIndex].SpawnArea.SpawnBlock();
                yield return new WaitForSeconds(_difficultyService.GetDifficultyLevel().Frequency);
            }
            onPackSpawned?.Invoke();
        }

        private void SpawnPack(int randomIndex)
        {
            _spawnPackBusy = true;
            _spawnAreas[randomIndex].SpawnArea.SpawnPack(_difficultyService.GetDifficultyLevel(), () => _spawnPackBusy = false);
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