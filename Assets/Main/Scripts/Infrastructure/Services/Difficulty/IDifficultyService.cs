namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public interface IDifficultyService : IService
    {
        DifficultyLevel GetDifficultyLevel();
        
        void IncreaseDifficulty();
    }
}