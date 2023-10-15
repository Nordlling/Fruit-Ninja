using System;

namespace Main.Scripts.Infrastructure.Services.Health
{
    public interface IHealthService : IService
    {
        int LeftHealths { get; }
        void DecreaseHealth();
        event Action OnDamaged;
        event Action OnDied;
    }
}