using System;

namespace Main.Scripts.Infrastructure.Services.GameOver
{
    public class GameOverService : IGameOverService
    {
        
        public event Action OnGameOver;

        public void GameOver()
        {
            OnGameOver?.Invoke();
        }
    }
}