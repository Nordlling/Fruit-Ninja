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
        private Vector2 _screenZoneSize;
        private Rect _livingRect;
        private Rect _screenRect;
        
        private Resolution _currentResolution;
        
        public bool IsInLivingZone(Vector2 position)
        {
            return _livingRect.Contains(position);
        }
        
        public Vector2 CalculateLocationWithinScreen(Vector2 halfRectSize, Vector2 position)
        {
            Vector2 minPosition = new Vector2(_screenRect.xMin, _screenRect.yMin);
            minPosition += halfRectSize;
            Vector2 maxPosition = new Vector2(_screenRect.xMax, _screenRect.yMax);
            maxPosition -= halfRectSize;
            
            float positionX = Mathf.Max(minPosition.x, position.x);
            if (positionX.Equals(position.x))
            {
                positionX = Mathf.Min(maxPosition.x, position.x);
            }
            
            float positionY = Mathf.Max(minPosition.y, position.y);
            if (positionY.Equals(position.y))
            {
                positionY = Mathf.Min(maxPosition.y, position.y);
            }

            return new Vector2(positionX, positionY);
        }

        private void Start()
        {
            _currentResolution = Screen.currentResolution;
            UpdateAllZones();
        }

        private void UpdateAllZones()
        {
            CalculateZonesSize();
            CalculateScreenRect();
            CalculateLivingRect();
        }

        private void Update()
        {
            if (ResolutionIsChanged())
            {
                UpdateAllZones();
            }
        }

        private bool ResolutionIsChanged()
        {
            return Screen.currentResolution.width != _currentResolution.width ||
                   Screen.currentResolution.height != _currentResolution.height;
        }

        private void OnValidate()
        {
            UpdateAllZones();
        }
        
        private void OnDrawGizmos()
        {
            UpdateAllZones();
            Gizmos.color = rectangleColor;
            Gizmos.DrawWireCube(_center, _livingZoneSize);
        }

        private void CalculateZonesSize()
        {
            _screenZoneSize = CalculateScreenSizeInWorldUnits();
            _livingZoneSize = _screenZoneSize + (_screenZoneSize * _deadOffset);
        }

        private void CalculateLivingRect()
        {
            _livingRect.width = _livingZoneSize.x;
            _livingRect.height = _livingZoneSize.y;
            _livingRect.center = _center;
        }
        
        private void CalculateScreenRect()
        {
            _screenRect.width = _screenZoneSize.x;
            _screenRect.height = _screenZoneSize.y;
            _screenRect.center = _center;
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