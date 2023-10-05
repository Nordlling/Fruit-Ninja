using System;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {

        private Dictionary<Type, IState> _states;
        private IState _activeState;
        

        public GameStateMachine(SceneLoader sceneLoader, ServiceContainer serviceContainer)
        {
            
            _states = new Dictionary<Type, IState>()
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader, serviceContainer) },
                { typeof(LoadSceneState), new LoadSceneState(this, sceneLoader, serviceContainer.Get<IGameFactory>()) },
                { typeof(GameLoopState), new GameLoopState() }
            };
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IState => 
            _states[typeof(TState)] as TState;
    }
}