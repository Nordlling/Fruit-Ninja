using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    public class SpawnArea : MonoBehaviour
    {
        private SpawnerAreaInfo _spawnerInfo;
        
        private Vector2 _firstPointPosition;
        private Vector2 _lastPointPosition;
        private Vector2 _normal;
        private Vector2 _newPoint;

        private float _leftTime;
        
        private Vector2 _screenOffset;

        public void Construct(SpawnerAreaInfo spawnerInfo)
        {
            _spawnerInfo = spawnerInfo;
        }

        private void Start()
        {
            _screenOffset = new Vector2(Screen.width * 0.1f, Screen.height * 0.1f);
            CalculateSpawnBounds();
        }

        public void SpawnFruit()
        {
            Vector2 direction = GenerateDirection();
            float speed = Random.Range(_spawnerInfo._minSpeed,_spawnerInfo. _maxSpeed);
            
            GameObject fruitObject = Instantiate(_spawnerInfo._throwableObjectPrefab, _newPoint, Quaternion.identity);
            Fruit fruit = fruitObject.GetComponent<Fruit>();
            fruit.Construct(direction, speed);
        }

        private void CalculateSpawnBounds()
        {
            Vector2 firstPointScreenPosition = new Vector2();
            Vector2 lastPointScreenPosition = new Vector2();
            float width = Screen.width;
            float height = Screen.height;
            
            switch (_spawnerInfo._spawnerPositionType)
            {
                case SpawnerPositionType.Bottom:
                    _normal = new Vector2(0, 1);
                    firstPointScreenPosition = new Vector2(0f + (width / 100 * _spawnerInfo._firstPointPercents), 0f - _screenOffset.y);
                    lastPointScreenPosition = new Vector2(0f + (width / 100 * _spawnerInfo._lastPointPercents), 0f - _screenOffset.y);
                    break;
                case SpawnerPositionType.Left:
                    _normal = new Vector2(1, 0);
                    firstPointScreenPosition = new Vector2(0f - _screenOffset.x, 0f + (height / 100 * _spawnerInfo._firstPointPercents));
                    lastPointScreenPosition = new Vector2(0f - _screenOffset.x, 0f + (height / 100 * _spawnerInfo._lastPointPercents));
                    break;
                case SpawnerPositionType.Right:
                    _normal = new Vector2(-1, 0);
                    firstPointScreenPosition = new Vector2(width + _screenOffset.x, 0f + (height / 100 * _spawnerInfo._firstPointPercents));
                    lastPointScreenPosition = new Vector2(width + _screenOffset.x, 0f + (height / 100 * _spawnerInfo._lastPointPercents));
                    break;
            }
            
            _firstPointPosition = Camera.main.ScreenToWorldPoint(firstPointScreenPosition);
            _lastPointPosition = Camera.main.ScreenToWorldPoint(lastPointScreenPosition);
           
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
            _newPoint = Vector2.Lerp(_firstPointPosition, _lastPointPosition, newPointValue);
        }

        private Vector2 GenerateAngle(Vector2 normal)
        {
            float angleDegrees = Random.Range(-_spawnerInfo._leftAngle, _spawnerInfo._rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * normal;
            return rotatedVector;
        }
    }
}
