using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Utils.RandomUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Block _blockPrefab;
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

            List<float> spawnWeights = _spawnAreas.Select(x => x.ProbabilityWeight).ToList();
            int randomIndex = spawnWeights.GetRandomWeightedIndex();
            if (randomIndex == -1)
            {
                Debug.LogError("Can't find weighted random index");
                randomIndex = Random.Range(0, spawnWeights.Count);
            }

            IDifficultyService difficultyService = ServiceContainer.Instance.Get<IDifficultyService>();
            difficultyService.IncreaseDifficulty();
            
            _spawnAreas[randomIndex].SpawnArea.SpawnPack(difficultyService.GetDifficultyLevel());
        
            _leftTime = Random.Range(_minInterval, _maxInterval) + difficultyService.GetDifficultyLevel().BlockCount * difficultyService.GetDifficultyLevel().Frequency;
        }

        private void CreateSpawnAreas()
        {
            foreach (SpawnInfo spawnAreaInfo in _spawnAreas)
            {
                SpawnArea spawnArea = Instantiate(_spawnerAreaPrefab);
                spawnAreaInfo.spawnerAreaInfo._blockPrefab = _blockPrefab;
                spawnArea.Construct(spawnAreaInfo.spawnerAreaInfo);
                spawnAreaInfo.SpawnArea = spawnArea;
            }
        }

        private void OnDrawGizmos()
        {
            foreach (SpawnInfo spawnInfo in _spawnAreas)
            {
                SetPosition(spawnInfo.spawnerAreaInfo);
                DrawSpawnField(spawnInfo);
                DrawAngles(spawnInfo);
            }
        }

        private void SetPosition(SpawnerAreaInfo spawnerInfo)
        {
            float currentScreenWidth = Camera.main.pixelWidth;
            float currentScreenHeight = Camera.main.pixelHeight;
            
            float firstPositionX = currentScreenWidth * spawnerInfo._firstPointXPercents;
            float firstPositionY = currentScreenHeight * spawnerInfo._firstPointYPercents;
            
            float lastPositionX = currentScreenWidth * spawnerInfo._lastPointXPercents;
            float lastPositionY = currentScreenHeight * spawnerInfo._lastPointYPercents;
            
            spawnerInfo._firstPoint = Camera.main.ScreenToWorldPoint(new Vector2(firstPositionX, firstPositionY));
            spawnerInfo._lastPoint = Camera.main.ScreenToWorldPoint(new Vector2(lastPositionX, lastPositionY));
        }

        private void DrawAngles(SpawnInfo spawnInfo)
        {
            Gizmos.color = Color.yellow;
            Vector2 firstPoint = spawnInfo.spawnerAreaInfo._firstPoint;
            Vector2 lastPoint = spawnInfo.spawnerAreaInfo._lastPoint;
            Vector2 lineVector = lastPoint - firstPoint;
            Vector2 centerPoint = Vector2.Lerp(firstPoint, lastPoint, 0.5f);
            Vector2 normal = new Vector2(-lineVector.y, lineVector.x).normalized;
            Vector2 rotatedLeftVector = Quaternion.Euler(0, 0, -(-spawnInfo.spawnerAreaInfo._leftAngle)) * normal;
            Vector2 rotatedRightVector = Quaternion.Euler(0, 0, -(spawnInfo.spawnerAreaInfo._rightAngle)) * normal;
            Gizmos.DrawRay(centerPoint, rotatedLeftVector.normalized);
            Gizmos.DrawRay(centerPoint, rotatedRightVector.normalized);
        }

        private void DrawSpawnField(SpawnInfo spawnInfo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(spawnInfo.spawnerAreaInfo._firstPoint, spawnInfo.spawnerAreaInfo._lastPoint);
        }
    }
}