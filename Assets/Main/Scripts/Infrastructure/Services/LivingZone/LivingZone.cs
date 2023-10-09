using System;
using UnityEngine;

namespace Main.Scripts
{
    public class LivingZone : MonoBehaviour
    {
        
        [SerializeField] private Color rectangleColor = Color.red;
        
        [Range(0, 10)]
        [SerializeField] private float _deadOffset;

        private readonly Vector2 _center = new Vector2(0, 0);
        private Vector2 _deadZoneSize;

        public bool IsInDeadZone(Vector2 position)
        {
            Vector2 halfSize = _deadZoneSize / 2f;
            Vector2 bottomLeftCorner = _center - halfSize;
            Vector2 topRightCorner = _center + halfSize;
            
            return position.x >= bottomLeftCorner.x && position.x <= topRightCorner.x &&
                   position.y >= bottomLeftCorner.y && position.y <= topRightCorner.y;
        }
        
        private void Start()
        {
            CalculateDeadZoneSize();
        }
        
        private void OnValidate()
        {
            CalculateDeadZoneSize();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = rectangleColor;
            CalculateDeadZoneSize();
            Gizmos.DrawWireCube(_center, _deadZoneSize);
        }

        private void CalculateDeadZoneSize()
        {
            Vector2 fieldSize = CalculateScreenSizeInWorldUnits();
            _deadZoneSize = fieldSize + (fieldSize * _deadOffset);
        }

        private Vector2 CalculateScreenSizeInWorldUnits()
        {
            float screenHeightInWorldUnits = 2f * Camera.main.orthographicSize;
            float screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;
            return new Vector2(screenWidthInWorldUnits, screenHeightInWorldUnits);
        }

    }
}