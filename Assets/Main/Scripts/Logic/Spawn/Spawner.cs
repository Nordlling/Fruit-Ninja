using System;
using System.Collections;
using System.Linq;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Boosters;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Samuraism;
using Main.Scripts.Utils.RandomUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Spawn
{
    public class Spawner : MonoBehaviour, IPlayable, ILoseable
    {
        public SpawnInfo[] SpawnAreas => _spawnAreas;

        [SerializeField] private float _minInterval;
        [SerializeField] private float _maxInterval;
        [SerializeField] private SpawnInfo[] _spawnAreas;

        private IDifficultyService _difficultyService;
        private IBlockFactory _blockFactory;
        private ISpawnFactory _spawnFactory;
        private ITimeProvider _timeProvider;
        private IBoostersCheckerService _boostersCheckerService;
        private ISamuraiService _samuraiService;

        private float _leftTime;
        private float[] _spawnWeights;

        private bool _spawnPackBusy;
        private bool _stop;

        public void Construct(
            IDifficultyService difficultyService,
            IBlockFactory blockFactory,
            ISpawnFactory spawnFactory,
            ITimeProvider timeProvider, 
            IBoostersCheckerService boostersCheckerService,
            ISamuraiService samuraiService
            )
        {
            _difficultyService = difficultyService;
            _blockFactory = blockFactory;
            _spawnFactory = spawnFactory;
            _timeProvider = timeProvider;
            _boostersCheckerService = boostersCheckerService;
            _samuraiService = samuraiService;
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

            _samuraiService.OnStarted += () => _leftTime = 0;
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

            if (!_samuraiService.Activated)
            {
                _difficultyService.IncreaseDifficulty();
            }
            
            StartCoroutine(SpawnPack(() => _spawnPackBusy = false));

            float PackFrequencyMultiplier = _samuraiService.SamuraiInfo.PackFrequencyMultiplier;
            _leftTime = Random.Range(_minInterval / PackFrequencyMultiplier, _maxInterval / PackFrequencyMultiplier);
        }
        
        private IEnumerator SpawnPack(Action onPackSpawned = null)
        {
            _spawnPackBusy = true;
            
            float blockFrequencyMultiplier = _samuraiService.SamuraiInfo.BlockFrequencyMultiplier;
            float spawnFrequency = _difficultyService.DifficultyLevel.Frequency / blockFrequencyMultiplier;
            
            int blockCount = _difficultyService.DifficultyLevel.BlockCount * _samuraiService.SamuraiInfo.BlockCountMultiplier;
            _boostersCheckerService.CalculateBlockMaxCounter(blockCount);
            
            int boostersMaxCounter = _boostersCheckerService.MaxCountInPack;
            int blockMaxCounter = blockCount - boostersMaxCounter;
            
            for (int i = 0; i < blockCount; i++)
            {
                float elapsedTime = 0f;

                if (!_samuraiService.Activated)
                {
                    Spawn(ref blockMaxCounter, ref boostersMaxCounter);
                }
                else
                {
                    int randomIndex = _spawnWeights.GetRandomWeightedIndex();
                    SpawnArea spawnArea = _spawnAreas[randomIndex].SpawnArea;
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

        private void Spawn(ref int blockMaxCounter, ref int boostersMaxCounter)
        {
            int randomIndex = _spawnWeights.GetRandomWeightedIndex();
            SpawnArea spawnArea = _spawnAreas[randomIndex].SpawnArea;

            if (Random.value > 0.5 && blockMaxCounter > 0)
            {
                spawnArea.SpawnBlock();
                blockMaxCounter--;
            }
            else
            {
                if (boostersMaxCounter > 0 && _boostersCheckerService.TrySpawnBooster(spawnArea))
                {
                    boostersMaxCounter--;
                }
                else
                {
                    spawnArea.SpawnBlock();
                    blockMaxCounter--;
                }
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