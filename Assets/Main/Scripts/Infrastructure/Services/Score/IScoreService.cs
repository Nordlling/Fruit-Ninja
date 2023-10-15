using System;

namespace Main.Scripts.Infrastructure.Services.Score
{
    public interface IScoreService : IService
    {
        event Action<int> OnScored;
        int CurrentScore { get; }
        int AddScore();
    }
}