using System;
using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    [Serializable]
    public class SpawnerAreaInfo
    {
        public SpawnerPositionType _spawnerPositionType;
        public GameObject _throwableObjectPrefab;

        [Range(0, 100)]
        public float _firstPointPercents;

        [Range(0, 100)]
        public float _lastPointPercents;

        [Range(-90, 90)]
        public float _leftAngle;

        [Range(-90, 90)]
        public float _rightAngle;

        public float _minSpeed;
        public float _maxSpeed;
    }
}