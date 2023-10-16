using Main.Scripts.Constants;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockMovement : MonoBehaviour
    {
        private Vector2 _startDirection;
        private float _speed;
        private Vector2 _gravityDirection;

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

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 direction = _startDirection * _speed;
            _currentPosition += direction * Time.deltaTime;
            _gravityDirection.y -= PhysicsConstants.Gravity * Time.deltaTime;
            _currentPosition += _gravityDirection * Time.deltaTime;
            transform.position = _currentPosition;
        }
    }

}
