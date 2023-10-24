using System;
using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class GameplayStateMachine : IGameplayStateMachine
    {
        
        private IGameplayState _activeState;
        private Dictionary<Type, IGameplayState> _states = new();

        public void AddState(IGameplayState state)
        {
            _states[state.GetType()] = state;
            state.StateMachine = this;
        }

        public void AddGameplayStatable(IGameplayStatable gameplayStatable)
        {
            foreach (IGameplayState gameplayState in _states.Values)
            {
                gameplayState.AddStatable(gameplayStatable);
            }
        }

        public void Enter<TState>() where TState : class, IGameplayState
        {
            IGameplayState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IGameplayState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IGameplayState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}