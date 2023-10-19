using System.Collections.Generic;
using Main.Scripts.Infrastructure.Provides;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PauseState : IGameplayState
    {
        private readonly SlowedTimeProvider _slowedTimeProvider;
        private List<IPauseable> _pauseables = new();


        public PauseState(SlowedTimeProvider slowedTimeProvider)
        {
            _slowedTimeProvider = slowedTimeProvider;
        }
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPauseable pauseable)
            {
                _pauseables.Add(pauseable); 
            }
        }

        public void Enter()
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                pauseable.Pause();
            }

            _slowedTimeProvider.TimeScale = 0f;
        }

        public void Exit()
        {
            _slowedTimeProvider.TimeScale = 1f;
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPauseable : IGameplayStatable
    {
        void Pause();
    }
}