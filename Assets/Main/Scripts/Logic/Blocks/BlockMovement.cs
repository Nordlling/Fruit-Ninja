using Main.Scripts.Constants;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockMovement : MonoBehaviour
    {
        private Vector2 _startDirection;
        private float _speed;
        private ITimeProvider _timeProvider;
        
        private Vector2 _gravity;
        private Vector2 _currentVelocity;
        private Vector2 _currentPosition;

        private readonly AdditionalForce _additionalForce = new();
        private MagnetConfig _magnetConfig;

        public void Construct(Vector3 startDirection, float speed, ITimeProvider timeProvider)
        {
            _startDirection = startDirection;
            _speed = speed;
            _timeProvider = timeProvider;
        }

        public void AddForceOnce(Vector2 forcedDirection)
        {
            _currentVelocity += forcedDirection;
        }
        
        public void AddAttraction(Vector2 attractionPosition, float duration, MagnetConfig magnetConfig)
        {
            _additionalForce.AttractionPosition = attractionPosition;
            _additionalForce.Force = magnetConfig.AttractionForce;
            _additionalForce.Duration = duration;
            _additionalForce.StrongRadius = magnetConfig.StrongRadius;
            _additionalForce.SingularityRadius = magnetConfig.SingularityRadius;
            _additionalForce.MaxRadius = Vector2.Distance(attractionPosition, transform.position);
            _additionalForce.MaxMagnetRadius = magnetConfig.AttractionRadius;
        }

        private void Start()
        {
            _currentPosition = transform.position;
            _currentVelocity = _startDirection * _speed;
            _gravity = PhysicsConstants.Gravity;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_additionalForce.Duration > 0)
            {
                AttractableMove();
                transform.position = _currentPosition;
            } 
            
            BasicMove();
            transform.position = _currentPosition;
        }

        private void BasicMove()
        {
            _currentVelocity += _gravity * _timeProvider.GetDeltaTime();
            _currentPosition += _currentVelocity * _timeProvider.GetDeltaTime();
        }

        private void AttractableMove()
        {
            Vector2 directionToAttraction = _additionalForce.AttractionPosition - _currentPosition;
            Vector2 normalizedDirection = directionToAttraction.normalized;
            float distance = directionToAttraction.magnitude;
            Vector2 force = normalizedDirection * _additionalForce.Force;

            if (distance > _additionalForce.MaxRadius * _additionalForce.StrongRadius)
            {
                _currentVelocity += force * _timeProvider.GetDeltaTime();
                _currentPosition += _currentVelocity * _timeProvider.GetDeltaTime();
            }
            
            if (distance <= _additionalForce.MaxRadius * _additionalForce.StrongRadius && distance > _additionalForce.MaxRadius * _additionalForce.SingularityRadius)
            {
                _currentVelocity = force;
                _currentPosition += _currentVelocity * _timeProvider.GetDeltaTime();
            }
            
            if (distance <= _additionalForce.MaxRadius * _additionalForce.SingularityRadius)
            {
                _currentVelocity = Vector2.zero;
                _currentPosition = _additionalForce.AttractionPosition;
            }

            _additionalForce.Duration -= _timeProvider.GetDeltaTime();

            if (_additionalForce.Duration <= 0f)
            {
                _currentVelocity = Vector2.zero;
            }
        }
    }

}
