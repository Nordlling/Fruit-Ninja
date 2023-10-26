using System;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.GameplayStates;

namespace Main.Scripts.Infrastructure.Services.Health
{
    public class HealthService : IHealthService, IRestartable
    {
        public event Action OnDecreased;
        public event Action OnIncreased;
        public event Action OnReset;

        private readonly HealthConfig _healthConfig;
        private readonly IGameplayStateMachine _gameplayStateMachine;

        private bool _block;

        public int LeftHealths { get; private set; }

        public HealthService(HealthConfig healthConfig, IGameplayStateMachine gameplayStateMachine)
        {
            _healthConfig = healthConfig;
            _gameplayStateMachine = gameplayStateMachine;
            
            InitHealth(healthConfig);
        }

        public void SwitchBlock(bool blocked)
        {
            _block = blocked;
        }

        public bool IsMaxHealth()
        {
            return LeftHealths == _healthConfig.HealthCount;
        }

        public void DecreaseHealth()
        {
            if (_block)
            {
                return;
            }
            
            LeftHealths--;
            OnDecreased?.Invoke();
            
            if (LeftHealths <= 0)
            {
                _gameplayStateMachine.Enter<LoseState>();
            }
        }
        
        public void IncreaseHealth()
        {
            LeftHealths++;
            OnIncreased?.Invoke();
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