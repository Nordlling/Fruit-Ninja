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
    public class GameplayUIInstaller : MonoInstaller
    {
        [SerializeField] private UIHealthView _uiHealthView;
        [SerializeField] private UIScoreView _uiScoreView;
        [SerializeField] private UIHighScoreView _uiHighScoreView;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            InitHealthUI(serviceContainer);
            InitScoreUI(serviceContainer);
            InitHighScoreUI(serviceContainer);
        }

        private void InitHealthUI(ServiceContainer serviceContainer)
        {
            _uiHealthView.Construct(serviceContainer.Get<IHealthService>());
        }
        
        private void InitScoreUI(ServiceContainer serviceContainer)
        {
            _uiScoreView.Construct(serviceContainer.Get<IScoreService>());
        }
        
        private void InitHighScoreUI(ServiceContainer serviceContainer)
        {
            _uiHighScoreView.Construct(serviceContainer.Get<IScoreService>());
        }
    }
}