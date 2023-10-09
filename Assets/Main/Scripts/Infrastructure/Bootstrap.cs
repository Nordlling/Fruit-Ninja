using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.States;
using UnityEngine;

namespace Main.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {

        [SerializeField] private BootstrapConfig _bootstrapConfig;
        private void Awake()
        {
            if (FindObjectsOfType<Bootstrap>().Length > 1)
            {
                return;
            }
            
            Game game = new Game(this, _bootstrapConfig);
            game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}