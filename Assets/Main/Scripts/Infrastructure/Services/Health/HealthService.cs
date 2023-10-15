using System;
using Main.Scripts.Infrastructure.Configs;

namespace Main.Scripts.Infrastructure.Services.Health
{
    public class HealthService : IHealthService
    {
        public event Action OnDamaged;
        public event Action OnDied;
        
        private readonly HealthConfig _healthConfig;
        
        public int LeftHealths { get; private set; }
        
        public HealthService(HealthConfig healthConfig)
        {
            _healthConfig = healthConfig;
            LeftHealths = healthConfig.HealthCount;
        }

        public void DecreaseHealth()
        {
            LeftHealths--;
            OnDamaged?.Invoke();
            
            if (LeftHealths <= 0)
            {
                OnDied?.Invoke();
            }
        }
    }
}