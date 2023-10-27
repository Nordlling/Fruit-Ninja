using System;
using Main.Scripts.Data;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Applications;
using Main.Scripts.Infrastructure.Services.SaveLoad;

namespace Main.Scripts.Infrastructure.Services.Score
{
    public class ScoreService : IScoreService, IRestartable
    {
        public event Action<int> OnScored;
        public event Action<int> OnHighScored;
        public event Action OnReset;

        private readonly ScoreConfig _scoreConfig;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IApplicationService _applicationService;

        private PlayerScore _playerScore;

        public ScoreService(ScoreConfig scoreConfig, ISaveLoadService saveLoadService, IApplicationService applicationService)
        {
            _scoreConfig = scoreConfig;
            _saveLoadService = saveLoadService;
            _applicationService = applicationService;
            LoadHighScore();

            _applicationService.OnPaused += SaveHighScore;
        }
        
        public ScoreService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            LoadHighScore();
        }

        public int CurrentScore { get; private set; }
        public int HighScore { get; private set; }

        public int AddScore()
        {
            return AddScore(_scoreConfig.BasicScoreValue);
        }
        
        public int AddScore(int score)
        {
            CurrentScore += score;
            OnScored?.Invoke(CurrentScore);

            if (CurrentScore > HighScore)
            {
                OnHighScored?.Invoke(CurrentScore);
                SaveHighScore();
            }
            
            return score;
        }

        public void Restart()
        {
            CurrentScore = 0;
            OnReset?.Invoke();
        }

        private void LoadHighScore()
        {
            _playerScore = _saveLoadService.LoadProgress() ?? new PlayerScore();
            HighScore = _playerScore.HighScore;
        }

        private void SaveHighScore()
        {
            HighScore = CurrentScore;
            _playerScore.HighScore = HighScore;
            _saveLoadService.SaveProgress(_playerScore);
        }
    }
}