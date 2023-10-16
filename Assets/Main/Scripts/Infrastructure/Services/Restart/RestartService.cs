using System;

namespace Main.Scripts.Infrastructure.Services.Restart
{
    public class RestartService : IRestartService
    {
        public event Action OnRestarted;

        public void Restart()
        {
            OnRestarted?.Invoke();
        }
    }
}