using System;
using System.Collections;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Logic.Blocks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Spawn
{
    public class SpawnArea : MonoBehaviour
    {
        private readonly float _distanceToOffset = 1f;
        
        private SpawnerAreaInfo _spawnerInfo;

        private Vector2 _normal;
        private Vector2 _newPointPosition;

        private ICollisionService _collisionService;
        private IGameFactory _gameFactory;

        public void Construct(SpawnerAreaInfo spawnerInfo, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _spawnerInfo = spawnerInfo;
        }
        
        public void SpawnPack(DifficultyLevel difficultyLevel, Action onPackSpawned)
        {
            StartCoroutine(StartSpawnPack(difficultyLevel, onPackSpawned));
        }

        private IEnumerator StartSpawnPack(DifficultyLevel difficultyLevel, Action onPackSpawned = null)
        {
            for (int i = 0; i < difficultyLevel.BlockCount; i++)
            {
                SpawnBlock();
                yield return new WaitForSeconds(difficultyLevel.Frequency);
            }
            onPackSpawned?.Invoke();
        }

        public void SpawnBlock()
        {
            GenerateNormal();
            Vector2 direction = GenerateDirection();
            OffsetPoint(direction);
            float speed = Random.Range(_spawnerInfo._minSpeed,_spawnerInfo. _maxSpeed);
            
            Block block = _gameFactory.CreateBlock(_spawnerInfo._blockPrefab, _newPointPosition);
            block.BlockMovement.Construct(direction, speed);
        }

        private void OffsetPoint(Vector2 direction)
        {
            _newPointPosition -= direction * _distanceToOffset;
        }
        
        private void GenerateNormal()
        {
            Vector2 lineVector = _spawnerInfo._lastPoint - _spawnerInfo._firstPoint;
            _normal = new Vector2(-lineVector.y, lineVector.x).normalized;
        }
        
        private Vector2 GenerateDirection()
        {
            GeneratePointPosition();
            Vector2 rotatedVector = GenerateAngle(_normal);
            return rotatedVector.normalized;
        }

        private void GeneratePointPosition()
        {
            float newPointValue = Random.Range(0f, 1f);
            _newPointPosition = Vector2.Lerp( _spawnerInfo._firstPoint, _spawnerInfo._lastPoint, newPointValue);
        }

        private Vector2 GenerateAngle(Vector2 normal)
        {
            float angleDegrees = Random.Range(-_spawnerInfo._leftAngle, _spawnerInfo._rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * normal;
            return rotatedVector;
        }
    }
}
