using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services;
using UnityEngine.SceneManagement;

namespace Main.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _serviceContainer;
        private readonly BootstrapConfig _bootstrapConfig;

        private string _sceneName;

        public GameStateMachine StateMachine { get; set; }
        
        public BootstrapState(SceneLoader sceneLoader, ServiceContainer serviceContainer, BootstrapConfig bootstrapConfig)
        {
            _sceneLoader = sceneLoader;
            _serviceContainer = serviceContainer;
            _bootstrapConfig = bootstrapConfig;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneName = _bootstrapConfig.StartCurrentScene ? _sceneName = SceneManager.GetActiveScene().name : _bootstrapConfig.InitialScene;
            _sceneLoader.Load(_sceneName, onLoaded: EnterLoadScene);
        }

        private void RegisterServices()
        {
            _serviceContainer.SetService<IGameStateMachine, GameStateMachine>(StateMachine);
        }

        public void Exit()
        {
        }

        private void EnterLoadScene()
        {
            StateMachine.Enter<LoadSceneState, string>(_bootstrapConfig.StartCurrentScene
                ? _sceneName
                : _bootstrapConfig.FirstGameScene);
        }
    }
}