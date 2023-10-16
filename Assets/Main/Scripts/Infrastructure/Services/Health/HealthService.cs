using System;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.GameOver;
using Main.Scripts.Infrastructure.Services.Restart;

namespace Main.Scripts.Infrastructure.Services.Health
{
    public class HealthService : IHealthService
    {
        public event Action OnDamaged;
        public event Action OnDied;
        public event Action OnReset;

        private readonly Action OnBlocksFell;
        
        private readonly HealthConfig _healthConfig;
        private readonly IGameOverService _gameOverService;
        private readonly ICollisionService _collisionService;
        private readonly IRestartService _restartService;

        public int LeftHealths { get; private set; }
        
        public HealthService(
            HealthConfig healthConfig, 
            IGameOverService gameOverService, 
            ICollisionService collisionService,
            IRestartService restartService)
        {
            _healthConfig = healthConfig;
            InitHealth(healthConfig);

            _gameOverService = gameOverService;
            _collisionService = collisionService;
            _restartService = restartService;

            OnBlocksFell += GameOver;
            _restartService.OnRestarted += ResetHealth;
        }

        private void ResetHealth()
        {
            LeftHealths = _healthConfig.HealthCount;
            OnReset?.Invoke();
        }

        private void InitHealth(HealthConfig healthConfig)
        {
            LeftHealths = healthConfig.HealthCount;
        }

        public void DecreaseHealth()
        {
            LeftHealths--;
            OnDamaged?.Invoke();
            
            if (LeftHealths <= 0)
            {
                OnDied?.Invoke();
                _collisionService.WaitFallBlocks(OnBlocksFell);
            }
        }

        private void GameOver()
        {
            _gameOverService.GameOver();
        }
    }
}