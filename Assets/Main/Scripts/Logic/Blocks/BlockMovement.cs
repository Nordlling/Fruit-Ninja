using Main.Scripts.Constants;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockMovement : MonoBehaviour, IChoppable
    {
        private Vector2 _startDirection;
        private float _speed;
        private Vector2 _gravityVelocity;

        private Vector2 _currentPosition;

        private Camera _camera;

        public void Construct(Vector3 startDirection, float speed)
        {
            _startDirection = startDirection;
            _speed = speed;
        }

        private void Start()
        {
            _currentPosition = transform.position;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _currentPosition += _startDirection * _speed * Time.deltaTime;
            _gravityVelocity.y -= PhysicsConstants.Gravity * Time.deltaTime;
            _currentPosition += _gravityVelocity * Time.deltaTime;
            transform.position = _currentPosition;
        }
    }

}
