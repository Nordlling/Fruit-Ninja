using System;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockAnimation : MonoBehaviour
    {
        [Header("Speed Settings")]
        [SerializeField] private float _minRotateSpeed;
        [SerializeField] private float _maxRotateSpeed;
        
        [Header("Scale Settings")]
        [SerializeField] private float _minScaleValue;
        [SerializeField] private float _maxScaleValue;
        [SerializeField] private float _minScaleSpeed;
        [SerializeField] private float _maxScaleSpeed;

        private ITimeProvider _timeProvider;
        
        private float _rotateSpeedValue;
        private int _rotateSign;

        private float _scaleSpeed;
        private Vector3 _initialScale;
        private Vector3 _targetScale;

        private float _currentTime;

        private readonly List<Action> _allAnimationActions = new();
        private readonly List<Action> _selectedAnimationActions = new();

        private void Start()
        {
            InitValues();

            FillAnimationList();
            SelectAnimations();
        }

        private void Update()
        {
            foreach (Action action in _selectedAnimationActions)
            {
                action.Invoke();
            }
        }

        private void InitValues()
        {
            _rotateSign = Random.value < 0.5 ? 1 : -1;
            _rotateSpeedValue = Random.Range(_minRotateSpeed, _maxRotateSpeed) * _rotateSign;

            _initialScale = transform.localScale;
            float scaleValue = Random.Range(_minScaleValue, _maxScaleValue);
            _targetScale = new Vector3(scaleValue, scaleValue, scaleValue);
            _scaleSpeed = Random.Range(_minScaleSpeed, _maxScaleSpeed);
        }

        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        private void FillAnimationList()
        {
            _allAnimationActions.Add(ExecuteRotateAnimation);
            _allAnimationActions.Add(ExecuteScaleAnimation);
        }

        private void SelectAnimations()
        {
            foreach (Action action in _allAnimationActions)
            {
                if (Random.value > 0.5)
                {
                    _selectedAnimationActions.Add(action);
                }
            }
        }

        private void ExecuteRotateAnimation()
        {
            transform.Rotate(0f, 0f, _rotateSpeedValue * _timeProvider.GetDeltaTime());
        }

        private void ExecuteScaleAnimation()
        {
            transform.localScale = Vector3.Lerp(_initialScale, _targetScale, _currentTime * _scaleSpeed);
            _currentTime += _timeProvider.GetDeltaTime();
        }
    }
}