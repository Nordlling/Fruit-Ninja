using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Spawn;
using Main.Scripts.Logic.Swipe;
using Main.Scripts.UI.Gameplay;
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
        }
    }
}