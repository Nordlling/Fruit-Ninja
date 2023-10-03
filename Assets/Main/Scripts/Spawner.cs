using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _minInterval;
        [SerializeField] private float _maxInterval;
        [SerializeField] private SpawnAreaInfo[] _spawnAreas;
    
        private float _leftTime;
    
        private void Start()
        {
            _leftTime = Random.Range(_minInterval, _maxInterval);
        }

        private void Update()
        {
            if (_leftTime > 0f)
            {
                _leftTime -= Time.deltaTime;
                return;
            }

            foreach (SpawnAreaInfo spawnAreaInfo in _spawnAreas)
            {
                float random = Random.Range(0f, 1f);
                if (random <= spawnAreaInfo.Probability)
                {
                    spawnAreaInfo.SpawnArea.SpawnFruit();
                }
            }
        
            _leftTime = Random.Range(_minInterval, _maxInterval);
        }
    }

    [Serializable]
    public class SpawnAreaInfo
    {
        public SpawnArea SpawnArea;
        public float Probability;
    }
}