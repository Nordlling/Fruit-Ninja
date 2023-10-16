namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public interface IDifficultyService : IService
    {
        DifficultyLevel DifficultyLevel { get; }
        
        void IncreaseDifficulty();
    }
}