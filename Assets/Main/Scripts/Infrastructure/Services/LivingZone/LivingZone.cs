using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.LivingZone
{
    public class LivingZone : MonoBehaviour
    {
        [SerializeField] private Color rectangleColor = Color.red;
        [Range(0, 10)]
        [SerializeField] private float _deadOffset;
        [SerializeField] private Camera _camera;

        private readonly Vector2 _center = new Vector2(0, 0);
        private Vector2 _livingZoneSize;
        private Rect _livingRect;
        
        public bool IsInLivingZone(Vector2 position)
        {
            return _livingRect.Contains(position);
        }

        private void Start()
        {
            CalculateDeadZoneSize();
            CalculateLivingRect();
        }

        private void OnValidate()
        {
            CalculateDeadZoneSize();
        }
        
        private void OnDrawGizmos()
        {
            CalculateDeadZoneSize();
            CalculateLivingRect();
            
            Gizmos.color = rectangleColor;
            Gizmos.DrawWireCube(_center, _livingZoneSize);
        }

        private void CalculateDeadZoneSize()
        {
            Vector2 fieldSize = CalculateScreenSizeInWorldUnits();
            _livingZoneSize = fieldSize + (fieldSize * _deadOffset);
        }

        private void CalculateLivingRect()
        {
            _livingRect.width = _livingZoneSize.x;
            _livingRect.height = _livingZoneSize.y;
            _livingRect.center = _center;
        }

        private Vector2 CalculateScreenSizeInWorldUnits()
        {
            if (_camera == null)
            {
                return new Vector2(0, 0);
            }
            float screenHeightInWorldUnits = 2f * _camera.orthographicSize;
            float screenWidthInWorldUnits = screenHeightInWorldUnits * _camera.aspect;
            return new Vector2(screenWidthInWorldUnits, screenHeightInWorldUnits);
        }
    }
}