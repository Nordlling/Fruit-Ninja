using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Menu;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuWindow _menuWindow;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterScoreService(serviceContainer);
            
            InitMenuWindow(serviceContainer);
        }
        
        private void RegisterScoreService(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<IScoreService, ScoreService>(new ScoreService(serviceContainer.Get<ISaveLoadService>()));
        }

        private void InitMenuWindow(ServiceContainer serviceContainer)
        {
            _menuWindow.Construct(serviceContainer.Get<IGameStateMachine>(), serviceContainer.Get<IScoreService>());
        }
    }
}