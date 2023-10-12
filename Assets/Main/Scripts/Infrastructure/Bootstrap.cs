using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;
using UnityEngine;

namespace Main.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private BootstrapConfig _bootstrapConfig;
        private IGameStateMachine _gameStateMachine;
        private void Awake()
        {
            // if (FindObjectsOfType<Bootstrap>().Length > 1)
            // {
            //     return;
            // }

            ServiceContainer serviceContainer = new ServiceContainer();
            SceneLoader sceneLoader = new SceneLoader(this);
            
            InitGameStateMachine(serviceContainer, sceneLoader);
            _gameStateMachine.Enter<BootstrapState>();
            
            // DontDestroyOnLoad(this);
        }

        private void InitGameStateMachine(ServiceContainer serviceContainer, SceneLoader sceneLoader)
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.AddState(new BootstrapState(sceneLoader, serviceContainer, _bootstrapConfig));
            _gameStateMachine.AddState(new LoadSceneState(sceneLoader, _sceneContext, serviceContainer));
            _gameStateMachine.AddState(new GameLoopState());
        }
    }
}