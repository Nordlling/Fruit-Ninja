using System;
using Main.Scripts.Data;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Score
{
    public class ScoreService : IScoreService
    {
        public event Action<int> OnScored;
        public event Action<int> OnHighScored;

        private readonly ScoreConfig _scoreConfig;
        private readonly ISaveLoadService _saveLoadService;

        private const string _highScoreKey = "highScore";

        private int _comboCounter = 1;
        private float _lastScoredTime;
        private PlayerProgress _playerProgress;

        public ScoreService(ScoreConfig scoreConfig, ISaveLoadService saveLoadService)
        {
            _scoreConfig = scoreConfig;
            _saveLoadService = saveLoadService;
            LoadHighScore();
        }

        public int CurrentScore { get; private set; }
        public int HighScore { get; private set; }

        public int AddScore()
        {
            int score = CalculateScore();
            CurrentScore += score;
            OnScored?.Invoke(CurrentScore);

            if (CurrentScore > HighScore)
            {
                OnHighScored?.Invoke(CurrentScore);
                SaveHighScore();
            }

            _lastScoredTime = Time.time;
            return score;
        }

        private void LoadHighScore()
        {
            _playerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
            HighScore = _playerProgress.HighScore;
        }

        private void SaveHighScore()
        {
            HighScore = CurrentScore;
            _playerProgress.HighScore = HighScore;
            _saveLoadService.SaveProgress(_playerProgress);
        }

        private int CalculateScore()
        {
            int score = _scoreConfig.BasicScoreValue;
            if (_comboCounter < _scoreConfig.MaxComboCount && Time.time - _lastScoredTime < _scoreConfig.MaxIntervalForCombo)
            {
                _comboCounter++;
                score *= _comboCounter * _scoreConfig.ComboMultiplier;
            }
            else
            {
                _comboCounter = 1;
            }

            return score;
        }
    }
}