using UnityEngine;

namespace Main.Scripts
{
    public class Fruit : MonoBehaviour
    {
        private const float _gravity = 9.81f;
    
        private Vector3 _startDirection;
        private float _speed;
        private Vector3 _gravityVelocity;
    
        private Vector3 _screenBounds;
        private Vector3 _currentPosition;
        private Vector2 _screenOffset;

        private Camera _camera;

        public void Construct(Vector3 startDirection, float speed)
        {
            _startDirection = startDirection;
            _speed = speed;
        }

        private void Start()
        {
            _camera = Camera.main;
            CalculateOffset();
            _screenBounds = new Vector3(Screen.width + _screenOffset.x, Screen.height + _screenOffset.y, 0);
        }

        private void FixedUpdate()
        {
            _currentPosition += _startDirection * _speed * Time.deltaTime;
            _gravityVelocity.y -= _gravity * Time.deltaTime;
            _currentPosition += _gravityVelocity * Time.deltaTime;
            transform.position = _currentPosition;

            if (BeyondBorders())
            {
                Destroy(gameObject);
            }
        }

        private void CalculateOffset()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Vector2 size = spriteRenderer.size;
            float distanceToCamera = (transform.position - _camera.transform.position).magnitude;
            float aspectRatio = Screen.width / (float)Screen.height;
            float screenWidth = size.x * Screen.width / (distanceToCamera * aspectRatio);
            float screenHeight = size.y * Screen.height / (distanceToCamera * aspectRatio);
            _screenOffset = new Vector2(screenWidth, screenHeight);
        }

        private bool BeyondBorders()
        {
            Vector3 screenPos = _camera.WorldToScreenPoint(_currentPosition);

            return screenPos.x < -_screenOffset.x || screenPos.x > _screenBounds.x ||
                   screenPos.y < -_screenOffset.y || screenPos.y > _screenBounds.y;
        }
    }
}
