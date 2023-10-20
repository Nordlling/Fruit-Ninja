using System.Linq;
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
        [SerializeField] private string _basicCounterText;
        [SerializeField] private bool _generateWordEnding;
        [SerializeField] private float _timeBeforeAnimation;
        [SerializeField] private float _animationDuration;

        private ITimeProvider _timeProvider;
        private LivingZone _livingZone;
        private Sequence _sequence;

        public void Construct(int multiplier, ITimeProvider timeProvider, LivingZone livingZone)
        {
            _counterValue.text = GenerateCounterText(multiplier);
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

        private void PlayAnimation()
        {

            _sequence = DOTween.Sequence()

                .Append(transform.DOScale(Vector3.one, _animationDuration))
                .AppendInterval(_timeBeforeAnimation)
                .Append(transform.DOScale(Vector3.zero, _animationDuration))
                .OnComplete(() => Destroy(gameObject));

            _sequence.Play();
            
            // DOTween.To(() => _scoreValue.alpha, x => _scoreValue.alpha = x, 0f, _animationFadeSpeed).OnKill(() => Debug.Log("End"));
            
        }

        private string GenerateCounterText(int multiplier)
        {
            string counterText = $"{multiplier} {_basicCounterText}";
            
            if (_generateWordEnding)
            {
                counterText += GenerateWordEnding(multiplier);
            }

            return counterText;
        }
        
        private string GenerateWordEnding(int multiplier)
        {
            int[] first = { 0, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
            int[] second = { 1 };
            int[] third = { 2, 3, 4 };

            if (first.Contains(multiplier))
            {
                return "ов";
            }
            
            if (second.Contains(multiplier))
            {
                return "";
            }
            
            if (third.Contains(multiplier))
            {
                return "а";
            }

            return "";
        }
    }
}