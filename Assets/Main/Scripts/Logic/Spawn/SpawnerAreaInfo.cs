using System;
using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    [Serializable]
    public class SpawnerAreaInfo
    {
        [Range(0, 1)]
        public float _firstPointX;
        [Range(0, 1)]
        public float _firstPointY;

        [Range(0, 1)]
        public float _lastPointX;
        [Range(0, 1)]
        public float _lastPointY;

        [Range(-180, 180)]
        public float _leftAngle;
        [Range(-180, 180)]
        public float _rightAngle;

        public float _minSpeed;
        public float _maxSpeed;
        
        [HideInInspector]
        public Vector2 _firstPoint;
        [HideInInspector]
        public Vector2 _lastPoint;
    }
}