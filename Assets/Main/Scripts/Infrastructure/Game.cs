using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;

namespace Main.Scripts.Infrastructure
{
    public class Game
    {
        public readonly IGameStateMachine GameStateMachine;
        
        public Game(ICoroutineRunner coroutineRunner)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), new ServiceContainer());
        }
        
    }
}