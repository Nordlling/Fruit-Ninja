using System;
using System.Collections;
using System.Linq;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Utils.RandomUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Spawn
{
    public class Spawner : MonoBehaviour, IPlayable, ILoseable
    {
        public SpawnInfo[] SpawnAreas => _spawnAreas;
        
        [SerializeField] private SpawnArea _spawnerAreaPrefab;

        [SerializeField] private float _minInterval;
        [SerializeField] private float _maxInterval;
        [SerializeField] private SpawnInfo[] _spawnAreas;

        private IDifficultyService _difficultyService;
        private IBlockFactory _blockFactory;
        private ITimeProvider _timeProvider;
        private ISpawnFactory _spawnFactory;

        private float _leftTime;
        private float[] _spawnWeights;

        private bool _spawnPackBusy;
        private bool _stop;
       

        public void Construct(IDifficultyService difficultyService, 
            IBlockFactory blockFactory,
            ISpawnFactory spawnFactory,
            ITimeProvider timeProvider)
        {
            _difficultyService = difficultyService;
            _blockFactory = blockFactory;
            _spawnFactory = spawnFactory;
            _timeProvider = timeProvider;
        }

        public void Play()
        {
            _stop = false;
        }

        public void Lose()
        {
            _stop = true;
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
                _leftTime -= _timeProvider.GetDeltaTime();
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
            float spawnFrequency = _difficultyService.DifficultyLevel.Frequency;
            for (int i = 0; i < _difficultyService.DifficultyLevel.BlockCount; i++)
            {
                float elapsedTime = 0f;
                int randomIndex = GenerateRandomIndex();
                _spawnAreas[randomIndex].SpawnArea.SpawnBlock();
                
                while (elapsedTime < spawnFrequency)
                {
                    yield return null;
                    elapsedTime += _timeProvider.GetDeltaTime();
                }
            }
            onPackSpawned?.Invoke();
        }

        private void CreateSpawnAreas()
        {
            foreach (SpawnInfo spawnAreaInfo in _spawnAreas)
            {
                SpawnArea spawnArea = _spawnFactory.CreateSpawnArea();
                spawnArea.Construct(spawnAreaInfo.spawnerAreaInfo, _blockFactory);
                spawnAreaInfo.SpawnArea = spawnArea;
            }
        }
    }
}