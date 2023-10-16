using System;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services.Restart;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public class DifficultyService : IDifficultyService
    {
        private readonly DifficultyConfig _difficultyConfig;
        private readonly IRestartService _restartService;

        private readonly int _levelsToIncreaseBlockCount;
        private readonly int _levelsToIncreaseFrequency;

        private int _leftLevelsToIncreaseBlockCount;
        private int _leftLevelsToIncreaseFrequency;

        public DifficultyService(DifficultyConfig difficultyConfig, IRestartService restartService)
        {
            _difficultyConfig = difficultyConfig;
            _restartService = restartService;

            _restartService.OnRestarted += InitDifficultyLevel;
            InitDifficultyLevel();
        }

        private void InitDifficultyLevel()
        {
            DifficultyLevel = new DifficultyLevel()
            {
                BlockCount = _difficultyConfig.InitialBlockCount,
                Frequency = _difficultyConfig.InitialFrequency
            };
            _leftLevelsToIncreaseBlockCount = _difficultyConfig.LevelsToIncreaseBlockCount;
            _leftLevelsToIncreaseFrequency = _difficultyConfig.LevelsToIncreaseFrequency;
        }

        public DifficultyLevel DifficultyLevel { get; private set; }

        public void IncreaseDifficulty()
        {
            IncreaseBlockCount();
            IncreaseFrequency();
        }

        private void IncreaseBlockCount()
        {
            if (DifficultyLevel.BlockCount == _difficultyConfig.MaxBlockCount)
            {
                return;
            }
            
            if (_leftLevelsToIncreaseBlockCount > 0)
            {
                _leftLevelsToIncreaseBlockCount--;
                return;
            }

            DifficultyLevel.BlockCount += _difficultyConfig.BlockCountProgression;
            DifficultyLevel.BlockCount = Mathf.Min(_difficultyConfig.MaxBlockCount, DifficultyLevel.BlockCount);
            _leftLevelsToIncreaseBlockCount = _difficultyConfig.LevelsToIncreaseBlockCount;
        }

        private void IncreaseFrequency()
        {
            if (Math.Abs(DifficultyLevel.Frequency - _difficultyConfig.MinFrequency) < float.Epsilon)
            {
                return;
            }
            
            if (_leftLevelsToIncreaseFrequency > 0)
            {
                _leftLevelsToIncreaseFrequency--;
                return;
            }
            
            DifficultyLevel.Frequency -= DifficultyLevel.Frequency * _difficultyConfig.FrequencyProgression;
            DifficultyLevel.Frequency = Mathf.Max(_difficultyConfig.MinFrequency, DifficultyLevel.Frequency);
            _leftLevelsToIncreaseFrequency = _levelsToIncreaseFrequency;
        }
    }
}