using System;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services.Health;

namespace Main.Scripts.Infrastructure.Services.Score
{
    public class ScoreService : IScoreService
    {
        public event Action<int> OnScored;
        
        private readonly ScoreConfig _scoreConfig;
        private readonly int _healthCount;

        public ScoreService(ScoreConfig scoreConfig)
        {
            _scoreConfig = scoreConfig;
        }

        public int CurrentScore { get; private set; }

        public int AddScore()
        {
            CurrentScore += _scoreConfig.BasicScoreValue;
            OnScored?.Invoke(CurrentScore);
            return _scoreConfig.BasicScoreValue;
        }
    }
}