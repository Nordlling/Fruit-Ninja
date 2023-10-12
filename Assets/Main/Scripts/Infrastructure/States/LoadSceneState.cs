using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public class LoadSceneState : IParametrizedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly SceneContext _sceneContext;
        private readonly ServiceContainer _serviceContainer;
        
        public GameStateMachine StateMachine { get; set; }

        public LoadSceneState(SceneLoader sceneLoader, SceneContext sceneContext, ServiceContainer serviceContainer)
        {
            _sceneLoader = sceneLoader;
            _sceneContext = sceneContext;
            _serviceContainer = serviceContainer;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitGameWorld();

            StateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _sceneContext.Setup(_serviceContainer);
        }
    }
}