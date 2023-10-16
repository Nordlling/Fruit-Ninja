using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.GameOver;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.Restart;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Gameplay;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameplayUIInstaller : MonoInstaller
    {
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private UIHealthView _uiHealthView;
        [SerializeField] private UIScoreView _uiScoreView;
        [SerializeField] private UIHighScoreView _uiHighScoreView;
        [SerializeField] private UIGameOverView _uiGameOverView;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            InitGameplayUI(serviceContainer);
            InitHealthUI(serviceContainer);
            InitScoreUI(serviceContainer);
            InitHighScoreUI(serviceContainer);
            InitGameOverUI(serviceContainer);
        }
        
        private void InitGameplayUI(ServiceContainer serviceContainer)
        {
            _gameplayUI.Construct(serviceContainer.Get<IGameOverService>());
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
        
        private void InitGameOverUI(ServiceContainer serviceContainer)
        {
            _uiGameOverView.Construct(
                serviceContainer.Get<IGameStateMachine>(), 
                serviceContainer.Get<IRestartService>(),
                serviceContainer.Get<IScoreService>());
        }
    }
}