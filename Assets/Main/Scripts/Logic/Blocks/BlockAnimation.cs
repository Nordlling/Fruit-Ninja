using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockAnimation : MonoBehaviour
    {
        [SerializeField] private float _minRotateSpeed;
        [SerializeField] private float _maxRotateSpeed;
        
        [SerializeField] private float _minScaleValue;
        [SerializeField] private float _maxScaleValue;
        [SerializeField] private float _minScaleSpeed;
        [SerializeField] private float _maxScaleSpeed;
        
        private Action OnRotate;
        private Action OnScale;
        private readonly List<Action> _animations = new();

        private void Start()
        {
            FillAnimationList();
            ExecuteAnimations();
        }

        private void FillAnimationList()
        {
            OnRotate += RotateObject;
            OnScale += ScaleObject;

            _animations.Add(OnRotate);
            _animations.Add(OnScale);
        }

        private void ExecuteAnimations()
        {
            foreach (Action action in _animations)
            {
                if (Random.value > 0.5)
                {
                    action.Invoke();
                }
            }
        }

        private void RotateObject()
        {
            int randomSign = Random.value < 0.5 ? 1 : -1;
            Vector3 randomRotateValue = new Vector3(0f, 0f, 360f * randomSign);
            float randomRotateDuration = Random.Range(_minRotateSpeed, _maxRotateSpeed);


            transform.DORotate(randomRotateValue, randomRotateDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }

        private void ScaleObject()
        {
            float randomScaleValue = Random.Range(_minScaleValue, _maxScaleValue);
            float randomScaleDuration = Random.Range(_minScaleSpeed, _maxScaleSpeed);

            transform.DOScale(randomScaleValue, randomScaleDuration)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }
    }
}