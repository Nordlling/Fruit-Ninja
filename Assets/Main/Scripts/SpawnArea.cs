using UnityEngine;

namespace Main.Scripts
{
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] private Transform _firstPoint;
        [SerializeField] private Transform _lastPoint;
    
        [SerializeField] private float _leftAngle;
        [SerializeField] private float _rightAngle;
    
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
    
        [SerializeField] private GameObject _objectPrefab;

        private float _leftTime;
        private Vector2 _newPoint;

        public void SpawnFruit()
        {
            Vector2 direction = CreateDirection();
            float speed = Random.Range(_minSpeed, _maxSpeed);
            GameObject fruitObject = Instantiate(_objectPrefab, _newPoint, Quaternion.identity);
            Fruit fruit = fruitObject.GetComponent<Fruit>();
            fruit.Construct(direction, speed);
        }
    
        private Vector2 CreateDirection()
        {
            GeneratePointPosition();
            Vector2 normal = CreateNormal();
            Vector2 rotatedVector = GenerateAngle(normal);
            return rotatedVector.normalized;
        }

        private void GeneratePointPosition()
        {
            float newPointValue = Random.Range(0f, 1f);
            _newPoint = Vector2.Lerp(_firstPoint.position, _lastPoint.position, newPointValue);
        }

        private Vector2 CreateNormal()
        {
            Vector2 lineVector = _lastPoint.position - _firstPoint.position;
            Vector2 normal = new Vector2(-lineVector.y, lineVector.x).normalized;
            return normal;
        }

        private Vector2 GenerateAngle(Vector2 normal)
        {
            float angleDegrees = Random.Range(-_leftAngle, _rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * normal;
            return rotatedVector;
        }
    }
}
