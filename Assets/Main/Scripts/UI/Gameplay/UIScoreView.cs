using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Score;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class UIScoreView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _scoreText;
        [SerializeField] protected float _scoreAnimationDuration;
        
        protected IScoreService _scoreService;
        private ITimeProvider _timeProvider;
        
        private int _currentScore;
        private Coroutine _currentCoroutine;
        private Tweener _currentTweener;

        public void Construct(IScoreService scoreService, ITimeProvider timeProvider)
        {
            _scoreService = scoreService;
            _timeProvider = timeProvider;
        }

        private void Start()
        {
            InitScore();
        }

        private void Update()
        {
            if (_currentTweener != null)
            {
                _currentTweener.timeScale = _timeProvider.GetTimeScale();
            }
        }

        private void InitScore()
        {
            _currentScore = 0;
            _scoreText.text = _currentScore.ToString();
        }

        private void OnEnable()
        {
            _scoreService.OnScored += AddScore;
            _scoreService.OnReset += InitScore;
        }

        private void OnDisable()
        {
            _scoreService.OnScored -= AddScore;
            _scoreService.OnReset -= InitScore;
        }

        protected void AddScore(int newScore)
        {
            _currentTweener?.Kill();
            _scoreText.text = _currentScore.ToString();
            _currentTweener = AnimateScoreChange(_currentScore, newScore);
            _currentScore = newScore;
        }
        
        private Tweener AnimateScoreChange(int oldScore, int newScore)
        {
            return DOTween
                .To(() => oldScore, x => oldScore = x, newScore, _scoreAnimationDuration)
                .OnUpdate(() => _scoreText.text = oldScore.ToString())
                .Play();
        }
    }
}