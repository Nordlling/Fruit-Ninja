using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class ScoreAnimation : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _scoreValue;
        [SerializeField] protected float _scoreAnimationDuration;
        
        private ITimeProvider _timeProvider;
        
        private int _currentScore;
        private Tweener _currentTweener;

        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        private void Update()
        {
            if (_currentTweener != null)
            {
                _currentTweener.timeScale = _timeProvider.GetTimeScale();
            }
        }

        public void AddScoreAnimation(int newScore)
        {
            _currentTweener?.Kill();
            _scoreValue.text = _currentScore.ToString();
            _currentTweener = AnimateScoreChange(_currentScore, newScore);
            _currentScore = newScore;
        }
        
        private Tweener AnimateScoreChange(int oldScore, int newScore)
        {
            return DOTween
                .To(() => oldScore, x => oldScore = x, newScore, _scoreAnimationDuration)
                .OnUpdate(() => _scoreValue.text = oldScore.ToString())
                .Play();
        }
    }
}