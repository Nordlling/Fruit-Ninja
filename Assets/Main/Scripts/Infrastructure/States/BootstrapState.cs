using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Config;
using UnityEngine.SceneManagement;

namespace Main.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _serviceContainer;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceContainer serviceContainer)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceContainer = serviceContainer;
            
            RegisterServices();;
        }

        private void RegisterServices()
        {
            RegisterConfigService();
            _serviceContainer.SetService<IGameFactory, GameFactory>(new GameFactory());
        }

        public void Enter()
        {
            GlobalConfig globalConfig = _serviceContainer.Get<IConfigService>().ForGlobalConfig();
            string sceneName = globalConfig.StartCurrentScene ? SceneManager.GetActiveScene().name : globalConfig.InitialScene;
            _sceneLoader.Load(sceneName, onLoaded: EnterLoadScene);
        }

        public void Exit()
        {
        }
        
        private void EnterLoadScene()
        {
            _stateMachine.Enter<LoadSceneState>();
        }
        
        private void RegisterConfigService()
        {
            ConfigService configService = new ConfigService();
            configService.Load();
            _serviceContainer.SetService<IConfigService, ConfigService>(configService);
        }
    }
}