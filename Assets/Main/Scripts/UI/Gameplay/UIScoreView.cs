using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Score;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class UIScoreView : MonoBehaviour
    {
        [SerializeField] protected ScoreAnimation _scoreAnimation;
        [SerializeField] protected TextMeshProUGUI _scoreText;
        
        protected IScoreService _scoreService;
        
        private int _currentScore;

        public void Construct(IScoreService scoreService, ITimeProvider timeProvider)
        {
            _scoreService = scoreService;
            _scoreAnimation.Construct(timeProvider);
        }

        private void Start()
        {
            InitScore();
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

        private void InitScore()
        {
            _currentScore = 0;
            _scoreText.text = _currentScore.ToString();
        }

        protected void AddScore(int newScore)
        {
            _scoreAnimation.AddScoreAnimation(newScore);
        }
    }
}