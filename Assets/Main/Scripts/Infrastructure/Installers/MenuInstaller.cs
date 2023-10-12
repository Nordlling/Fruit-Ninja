using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuWindow _menuWindow;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            InitMenuWindow(serviceContainer);
            
        }

        private void InitMenuWindow(ServiceContainer serviceContainer)
        {
            _menuWindow.Construct(serviceContainer.Get<IGameStateMachine>());
        }
    }
}