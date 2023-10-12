using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Difficulty;
using UnityEngine.SceneManagement;

namespace Main.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _serviceContainer;
        private readonly BootstrapConfig _bootstrapConfig;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceContainer serviceContainer, BootstrapConfig bootstrapConfig)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceContainer = serviceContainer;
            _bootstrapConfig = bootstrapConfig;

            RegisterServices();;
        }

        private void RegisterServices()
        {
        }

        public void Enter()
        {
            string sceneName = _bootstrapConfig.StartCurrentScene ? SceneManager.GetActiveScene().name : _bootstrapConfig.InitialScene;
            _sceneLoader.Load(sceneName, onLoaded: EnterLoadScene);
        }

        public void Exit()
        {
        }
        
        private void EnterLoadScene()
        {
            _stateMachine.Enter<LoadSceneState>();
        }
    }
}