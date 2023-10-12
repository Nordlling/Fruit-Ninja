using System;
using Main.Scripts.Infrastructure.Configs;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public class DifficultyService : IDifficultyService
    {
        private readonly DifficultyConfig _difficultyConfig;
        private readonly DifficultyLevel _difficultyLevel;

        private readonly int _levelsToIncreaseBlockCount;
        private readonly int _levelsToIncreaseFrequency;
        
        private int _leftLevelsToIncreaseBlockCount;
        private int _leftLevelsToIncreaseFrequency;
        
        public DifficultyService(DifficultyConfig difficultyConfig, DifficultyLevel difficultyLevel)
        {
            _difficultyConfig = difficultyConfig;
            _difficultyLevel = difficultyLevel;
            _leftLevelsToIncreaseBlockCount = difficultyConfig.LevelsToIncreaseBlockCount;
            _leftLevelsToIncreaseFrequency = difficultyConfig.LevelsToIncreaseFrequency;
        }

        public DifficultyLevel GetDifficultyLevel()
        {
            return _difficultyLevel;
        }

        public void IncreaseDifficulty()
        {
            IncreaseBlockCount();
            IncreaseFrequency();
        }

        private void IncreaseBlockCount()
        {
            if (_difficultyLevel.BlockCount == _difficultyConfig.MaxBlockCount)
            {
                return;
            }
            
            if (_leftLevelsToIncreaseBlockCount > 0)
            {
                _leftLevelsToIncreaseBlockCount--;
                return;
            }

            _difficultyLevel.BlockCount += _difficultyConfig.BlockCountProgression;
            _difficultyLevel.BlockCount = Mathf.Min(_difficultyConfig.MaxBlockCount, _difficultyLevel.BlockCount);
            _leftLevelsToIncreaseBlockCount = _difficultyConfig.LevelsToIncreaseBlockCount;
        }

        private void IncreaseFrequency()
        {
            if (Math.Abs(_difficultyLevel.Frequency - _difficultyConfig.MinFrequency) < float.Epsilon)
            {
                return;
            }
            
            if (_leftLevelsToIncreaseFrequency > 0)
            {
                _leftLevelsToIncreaseFrequency--;
                return;
            }
            
            _difficultyLevel.Frequency -= _difficultyLevel.Frequency * _difficultyConfig.FrequencyProgression;
            _difficultyLevel.Frequency = Mathf.Max(_difficultyConfig.MinFrequency, _difficultyLevel.Frequency);
            _leftLevelsToIncreaseFrequency = _levelsToIncreaseFrequency;
        }
    }
}