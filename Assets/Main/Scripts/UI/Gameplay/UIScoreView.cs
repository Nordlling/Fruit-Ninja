using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Score;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class UIScoreView : MonoBehaviour
    {
        [SerializeField] protected ScoreAnimation _scoreAnimation;
        [SerializeField] protected TextMeshProUGUI _scoreValue;
        
        protected IScoreService _scoreService;

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
            _scoreAnimation.ResetScore();
            _scoreValue.text = 0.ToString();
        }

        protected void AddScore(int newScore)
        {
            _scoreAnimation.AddScoreAnimation(newScore);
        }
    }
}