using System;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.GameplayStates;

namespace Main.Scripts.Infrastructure.Services.Health
{
    public class HealthService : IHealthService, IRestartable
    {
        public event Action OnDamaged;
        public event Action OnReset;
        
        private readonly HealthConfig _healthConfig;
        private readonly IGameplayStateMachine _gameplayStateMachine;

        public int LeftHealths { get; private set; }
        
        public HealthService(HealthConfig healthConfig, IGameplayStateMachine gameplayStateMachine)
        {
            _healthConfig = healthConfig;
            _gameplayStateMachine = gameplayStateMachine;
            
            InitHealth(healthConfig);
        }

        public void DecreaseHealth()
        {
            LeftHealths--;
            OnDamaged?.Invoke();
            
            if (LeftHealths <= 0)
            {
                _gameplayStateMachine.Enter<LoseState>();
            }
        }

        public void Restart()
        {
            ResetHealth();
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
    }
}