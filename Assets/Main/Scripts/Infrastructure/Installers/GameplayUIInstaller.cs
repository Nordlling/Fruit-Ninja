using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.AnimationTargetContainer;
using Main.Scripts.Infrastructure.Services.ButtonContainer;
using Main.Scripts.Infrastructure.Services.Health;
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
        [SerializeField] private UIPauseView _uiPauseView;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            InitGameplayUI(serviceContainer);
            InitHealthUI(serviceContainer);
            InitScoreUI(serviceContainer);
            InitHighScoreUI(serviceContainer);
            InitGameOverUI(serviceContainer);
            InitPauseUI(serviceContainer);
        }
        
        private void InitGameplayUI(ServiceContainer serviceContainer)
        {
            _gameplayUI.Construct
            (
                serviceContainer.Get<IGameplayStateMachine>(), 
                serviceContainer.Get<IButtonContainerService>()
            );

            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(_gameplayUI);
        }

        private void InitHealthUI(ServiceContainer serviceContainer)
        {
            _uiHealthView.Construct(serviceContainer.Get<IHealthService>(), serviceContainer.Get<IAnimationTargetContainer>());
        }
        
        private void InitScoreUI(ServiceContainer serviceContainer)
        {
            _uiScoreView.Construct(serviceContainer.Get<IScoreService>(), serviceContainer.Get<ITimeProvider>());
        }
        
        private void InitHighScoreUI(ServiceContainer serviceContainer)
        {
            _uiHighScoreView.Construct(serviceContainer.Get<IScoreService>(), serviceContainer.Get<ITimeProvider>());
        }
        
        private void InitGameOverUI(ServiceContainer serviceContainer)
        {
            _uiGameOverView.Construct
            (
                serviceContainer.Get<IGameStateMachine>(),
                serviceContainer.Get<IGameplayStateMachine>(),
                serviceContainer.Get<IScoreService>(),
                serviceContainer.Get<IButtonContainerService>()
            );
        }

        private void InitPauseUI(ServiceContainer serviceContainer)
        {
            _uiPauseView.Construct
            (
                serviceContainer.Get<IGameStateMachine>(),
                serviceContainer.Get<IGameplayStateMachine>(),
                serviceContainer.Get<IButtonContainerService>()
            );
        }
    }
}