using System;

namespace Main.Scripts.Infrastructure.Services.Health
{
    public interface IHealthService : IService
    {
        int LeftHealths { get; }
        
        event Action OnDecreased;
        event Action OnIncreased;
        event Action OnReset;
        
        bool IsMaxHealth();
        void DecreaseHealth();
        void IncreaseHealth();
        void SwitchBlock(bool blocked);
        
    }
}