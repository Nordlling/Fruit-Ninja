using System;

namespace Main.Scripts.Infrastructure.Services.Health
{
    public interface IHealthService : IService
    {
        int LeftHealths { get; }
        bool IsMaxHealth();
        void DecreaseHealth();
        void IncreaseHealth();
        event Action OnDecreased;
        event Action OnIncreased;
        event Action OnReset;
    }
}