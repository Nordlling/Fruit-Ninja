using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;

namespace Main.Scripts.Infrastructure
{
    public class Game
    {
        public readonly IGameStateMachine GameStateMachine;
        
        public Game(ICoroutineRunner coroutineRunner, BootstrapConfig _bootstrapConfig)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), new ServiceContainer(), _bootstrapConfig);
        }
        
    }
}