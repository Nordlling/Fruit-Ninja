using System.Collections.Generic;
using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.LivingZone;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Logic.Combo
{
    public class ComboLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterValue;
        [SerializeField] private TextMeshProUGUI _seriesValue;
        [SerializeField] private Transform _panelTransform;
        [SerializeField] private RectTransform _panelRectTransform;
        [SerializeField] private float _timeBeforeAnimation;
        [SerializeField] private float _animationDuration;

        private ITimeProvider _timeProvider;
        private LivingZone _livingZone;
        private Sequence _sequence;

        public void Construct(int multiplier, ITimeProvider timeProvider, LivingZone livingZone, Dictionary<int, string> fruitDictionary)
        {
            _counterValue.text = CreateCounterValue(multiplier, fruitDictionary);
            _seriesValue.text = multiplier.ToString();
            _timeProvider = timeProvider;
            _livingZone = livingZone;
        }

        private void Start()
        {
            Vector2 halfRectSize = (_panelRectTransform.rect.size * _panelTransform.localScale) / 2;
            Vector2 correctedPosition = _livingZone.CalculateLocationWithinScreen(halfRectSize, transform.position);
            transform.position = correctedPosition;
            
            transform.localScale = Vector3.zero;
            PlayAnimation();
        }

        private void Update()
        {
            _sequence.timeScale = _timeProvider.GetTimeScale();
        }

        private string CreateCounterValue(int multiplier, Dictionary<int, string> fruitDictionary)
        {
            return !fruitDictionary.TryGetValue(multiplier, out string value) ? 
                multiplier.ToString() : $"{multiplier} {value}";
        }

        private void PlayAnimation()
        {
            _sequence = DOTween.Sequence()

                .Append(transform.DOScale(Vector3.one, _animationDuration))
                .AppendInterval(_timeBeforeAnimation)
                .Append(transform.DOScale(Vector3.zero, _animationDuration))
                .OnComplete(() => Destroy(gameObject));

            _sequence.Play();
        }
    }
}