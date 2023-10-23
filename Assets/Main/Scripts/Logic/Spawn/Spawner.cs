using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Infrastructure.Configs;
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
        private ISpawnFactory _spawnFactory;
        private ITimeProvider _timeProvider;
        private BoostersConfig _boostersConfig;

        private float _leftTime;
        private float[] _spawnWeights;

        private bool _spawnPackBusy;
        private bool _stop;
        
        private float[] _boosterWeights;
        private Dictionary<BoosterType, int> _boosterCounters = new();


        public void Construct(
            IDifficultyService difficultyService,
            IBlockFactory blockFactory,
            ISpawnFactory spawnFactory,
            ITimeProvider timeProvider, 
            BoostersConfig boostersConfig)
        {
            _difficultyService = difficultyService;
            _blockFactory = blockFactory;
            _spawnFactory = spawnFactory;
            _timeProvider = timeProvider;
            _boostersConfig = boostersConfig;
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
            _boosterWeights = _boostersConfig.Boosters.Select(x => x.BoosterSpawnInfo.DropoutRate).ToArray();
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

        private int GenerateRandomIndex(float[] weights)
        {
            int randomIndex = weights.GetRandomWeightedIndex();
            if (randomIndex == -1)
            {
                Debug.LogError("Can't find weighted random index");
                randomIndex = Random.Range(0, weights.Length);
            }

            return randomIndex;
        }
        
        private IEnumerator SpawnPack( Action onPackSpawned = null)
        {
            _spawnPackBusy = true;
            float spawnFrequency = _difficultyService.DifficultyLevel.Frequency;
            CalculateBlockMaxCounter();
            for (int i = 0; i < _difficultyService.DifficultyLevel.BlockCount; i++)
            {
                float elapsedTime = 0f;
                
                int randomIndex = GenerateRandomIndex(_spawnWeights);
                SpawnArea spawnArea = _spawnAreas[randomIndex].SpawnArea;
                
                if (!TrySpawnBooster(spawnArea))
                {
                    spawnArea.SpawnBlock();
                }
                
                while (elapsedTime < spawnFrequency)
                {
                    yield return null;
                    elapsedTime += _timeProvider.GetDeltaTime();
                }
            }
            onPackSpawned?.Invoke();
        }

        private void CalculateBlockMaxCounter()
        {
            foreach (BoosterInfo boosterInfo in _boostersConfig.Boosters)
            {
                BoosterSpawnInfo boosterSpawnInfo = boosterInfo.BoosterSpawnInfo;
                
                int blockMaxCounter = (int)(_difficultyService.DifficultyLevel.BlockCount * boosterSpawnInfo.MaxFractionInPack);

                if (boosterSpawnInfo.MaxNumberInPack != -1)
                {
                    blockMaxCounter = Mathf.Min(blockMaxCounter, boosterSpawnInfo.MaxNumberInPack);
                }

                if (boosterSpawnInfo.MaxNumberOnScreen != -1)
                {
                    blockMaxCounter = Mathf.Min(blockMaxCounter, boosterSpawnInfo.MaxNumberOnScreen);
                }

                _boosterCounters[boosterInfo.BoosterType] = blockMaxCounter;
            }
        }

        private bool TrySpawnBooster(SpawnArea spawnArea)
        {
            foreach (BoosterInfo boosterInfo in _boostersConfig.Boosters)
            {
                if (_boosterCounters[boosterInfo.BoosterType] > 0 && Random.Range(0f, 1f) < boosterInfo.BoosterSpawnInfo.DropoutRate)
                {
                    SpawnBooster(boosterInfo.BoosterType, spawnArea);
                    _boosterCounters[boosterInfo.BoosterType]--;
                    return true;
                }
            }

            return false;

        }

        private void SpawnBooster(BoosterType boosterType, SpawnArea spawnArea)
        {
            switch (boosterType)
            {
                case BoosterType.Bomb:
                    spawnArea.SpawnBomb();
                    break;
                case BoosterType.BonusLife:
                    Debug.Log($"Spawn {boosterType}");
                    break;
                case BoosterType.BlockBug:
                    Debug.Log($"Spawn {boosterType}");
                    break;
                case BoosterType.Freeze:
                    Debug.Log($"Spawn {boosterType}");
                    break;
                case BoosterType.Magnet:
                    Debug.Log($"Spawn {boosterType}");
                    break;
                case BoosterType.Brick:
                    Debug.Log($"Spawn {boosterType}");
                    break;
                case BoosterType.Samurai:
                    Debug.Log($"Spawn {boosterType}");
                    break;
                case BoosterType.Mimic:
                    Debug.Log($"Spawn {boosterType}");
                    break;
            }
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