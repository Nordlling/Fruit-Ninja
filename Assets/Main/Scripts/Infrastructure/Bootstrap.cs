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
        private IGameStateMachine GameStateMachine;
        private void Awake()
        {
            if (FindObjectsOfType<Bootstrap>().Length > 1)
            {
                return;
            }

            ServiceContainer serviceContainer = new ServiceContainer();
            _sceneContext.Setup(serviceContainer);
            GameStateMachine = new GameStateMachine(new SceneLoader(this), serviceContainer, _bootstrapConfig);
            GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}