using System;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    [Serializable]
    public class SpawnerAreaInfo
    {
        [Range(0, 1)]
        public float _firstPointXPercents;
        [Range(0, 1)]
        public float _firstPointYPercents;

        [Range(0, 1)]
        public float _lastPointXPercents;
        [Range(0, 1)]
        public float _lastPointYPercents;

        [Range(-180, 180)]
        public float _leftAngle;
        [Range(-180, 180)]
        public float _rightAngle;

        public float _minSpeed;
        public float _maxSpeed;
        
        [HideInInspector]
        public Block _blockPrefab;
        [HideInInspector]
        public Vector2 _firstPoint;
        [HideInInspector]
        public Vector2 _lastPoint;
    }
}