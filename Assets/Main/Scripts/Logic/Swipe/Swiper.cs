using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Swipe
{
    public class Swiper : MonoBehaviour, ISwiper, IPlayable, IGameOverable
    {
        public float Speed { get; private set; }
        public Vector2 Direction { get; private set; }
        public Vector2 Position => transform.position;

        [SerializeField] private float _minSpeedToSlice;
        [SerializeField] private TrailRenderer _firstTrailRenderer;
        [SerializeField] private TrailRenderer _secondTrailRenderer;

        private Camera _camera;
        private ITimeProvider _timeProvider;
        private Vector2 _lastPosition;
        private bool _touched;
        private Vector2 _currentPosition;
        private bool _stop;

        public void Construct(Camera mainCamera, ITimeProvider timeProvider)
        {
            _camera = mainCamera;
            _timeProvider = timeProvider;
        }

        public bool HasEnoughSpeed()
        {
            return Speed >= _minSpeedToSlice;
        }

        public void Block()
        {
            DisableSwiper();
        }

        public void Play()
        {
            _stop = false;
        }

        public void GameOver()
        {
            _stop = true;
        }

        private void Start()
        {
            SwitchTrails(false);
            _currentPosition = transform.position;
            _lastPosition = _currentPosition;
        }

        private void Update()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _currentPosition = mousePosition;
            
            if (Input.GetMouseButtonUp(0))
            {
                DisableSwiper();
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                EnableSwiper();
                return;
            }
            
            if (_touched && !_stop && _timeProvider.GetTimeScale() != 0f && Input.GetMouseButton(0))
            {
                SwitchTrails(true);
                Speed = Vector2.Distance(_lastPosition, _currentPosition) / Time.deltaTime;
                Direction = _currentPosition - _lastPosition;
            }
            
            _lastPosition = _currentPosition;
            transform.position = _currentPosition;
        }

        private void EnableSwiper()
        {
            _touched = true;
            _lastPosition = _currentPosition;
            transform.position = _currentPosition;
        }

        private void DisableSwiper()
        {
            _touched = false;
            SwitchTrails(false);
            Speed = 0f;
            Direction = Vector2.zero;
        }

        private void SwitchTrails(bool enable)
        {
            _firstTrailRenderer.enabled = enable;
            _secondTrailRenderer.enabled = enable;
        }
    }
}