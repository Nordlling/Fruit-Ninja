using System;

namespace Main.Scripts.Infrastructure.Services.Score
{
    public interface IScoreService : IService
    {
        event Action<int> OnScored;
        event Action<int> OnHighScored;
        int CurrentScore { get; }
        int HighScore { get; }
        int AddScore();
    }
}