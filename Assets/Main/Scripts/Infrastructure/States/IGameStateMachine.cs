using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
    }
}