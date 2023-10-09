namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public class DifficultyService : IDifficultyService
    {
        private readonly DifficultyLevel _difficultyLevel;

        private readonly int _levelsToIncreaseBlockCount;
        private readonly int _levelsToIncreaseFrequency;
        
        private readonly int _blockCountProgression;
        private readonly float _frequencyProgression;
        
        private int _leftLevelsToIncreaseBlockCount;
        private int _leftLevelsToIncreaseFrequency;
        public DifficultyService(DifficultyLevel difficultyLevel, int levelsToIncreaseBlockCount,
            int levelsToIncreaseFrequency, int blockCountProgression, float frequencyProgression)
        {
            _difficultyLevel = difficultyLevel;
            _levelsToIncreaseBlockCount = levelsToIncreaseBlockCount;
            _levelsToIncreaseFrequency = levelsToIncreaseFrequency;
            _leftLevelsToIncreaseBlockCount = levelsToIncreaseBlockCount;
            _leftLevelsToIncreaseFrequency = levelsToIncreaseFrequency;
            _blockCountProgression = blockCountProgression;
            _frequencyProgression = frequencyProgression;
        }

        public DifficultyLevel GetDifficultyLevel()
        {
            return _difficultyLevel;
        }

        public void IncreaseDifficulty()
        {
            _leftLevelsToIncreaseBlockCount--;
            if (_leftLevelsToIncreaseBlockCount <= 0)
            {
                _difficultyLevel.BlockCount += _blockCountProgression;
                _leftLevelsToIncreaseBlockCount = _levelsToIncreaseBlockCount;
            }
            
            _leftLevelsToIncreaseFrequency--;
            if (_leftLevelsToIncreaseFrequency <= 0)
            {
                _difficultyLevel.Frequency -= _difficultyLevel.Frequency * _frequencyProgression;
                _leftLevelsToIncreaseFrequency = _levelsToIncreaseFrequency;
            }
        }
    }
}