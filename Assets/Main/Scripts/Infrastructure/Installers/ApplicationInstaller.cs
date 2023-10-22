using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private ApplicationConfig _applicationConfig;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            InitApplicationSettings();
        }

        private void InitApplicationSettings()
        {
            Application.targetFrameRate = _applicationConfig.TargetFPS;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}