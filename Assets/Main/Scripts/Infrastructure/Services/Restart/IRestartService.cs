using System;

namespace Main.Scripts.Infrastructure.Services.Restart
{
    public interface IRestartService : IService
    {
        event Action OnRestarted;
        
        void Restart();
    }
}