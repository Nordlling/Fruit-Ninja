using Main.Scripts.Infrastructure.States;
using UnityEngine;

namespace Main.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            if (FindObjectsOfType<Bootstrap>().Length > 1)
            {
                return;
            }
            
            Game game = new Game(this);
            game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}