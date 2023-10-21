using System;

namespace Main.Scripts.Infrastructure.Services.Score
{
    public interface IScoreService : IService
    {
        event Action<int> OnScored;
        event Action<int> OnHighScored;
        event Action OnReset;
        int CurrentScore { get; }
        int HighScore { get; }
        int AddScore();
        int AddScore(int score);
    }
}