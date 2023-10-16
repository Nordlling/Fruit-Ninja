using System;

namespace Main.Scripts.Infrastructure.Services.GameOver
{
    public interface IGameOverService : IService
    {
        event Action OnGameOver;
        
        void GameOver();
    }
}