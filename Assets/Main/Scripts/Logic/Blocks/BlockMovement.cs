using Main.Scripts.Constants;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockMovement : MonoBehaviour
    {
        private Vector2 _startDirection;
        private float _speed;
        private ITimeProvider _timeProvider;
        
        private Vector2 _gravityDirection;
        private Vector2 _currentPosition;

        public void Construct(Vector3 startDirection, float speed, ITimeProvider timeProvider)
        {
            _startDirection = startDirection;
            _speed = speed;
            _timeProvider = timeProvider;
        }

        private void Start()
        {
            _currentPosition = transform.position;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 direction = _startDirection * _speed;
            _currentPosition += direction * _timeProvider.GetDeltaTime();
            _gravityDirection.y -= PhysicsConstants.Gravity * _timeProvider.GetDeltaTime();
            _currentPosition += _gravityDirection * _timeProvider.GetDeltaTime();
            transform.position = _currentPosition;
        }
    }

}
