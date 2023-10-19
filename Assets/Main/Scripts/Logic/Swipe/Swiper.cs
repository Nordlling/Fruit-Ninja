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
        [SerializeField] private TrailRenderer _trailRenderer;

        private Camera _camera;
        private ITimeProvider _timeProvider;
        private Vector3 _lastPosition;
        private bool _touched;
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
            _lastPosition = transform.position;
        }

        private void Update()
        {
            if (!_stop && _timeProvider.GetTimeScale() != 0f && Input.GetMouseButton(0))
            {
                _trailRenderer.enabled = true;
                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePosition;
                if (!_touched)
                {
                    _trailRenderer.enabled = false;
                    _lastPosition = transform.position;
                }
                _touched = true;
            }
            else
            {
                _touched = false;
            }
            Speed = Vector2.Distance(_lastPosition, transform.position) / Time.deltaTime;
            Direction = transform.position - _lastPosition;
            _lastPosition = transform.position;
        }
    }
}