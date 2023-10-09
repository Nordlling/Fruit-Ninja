using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Spawn;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [SerializeField] private LivingZone _livingZone;
        [SerializeField] private Spawner _spawner;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(_livingZone);
            serviceContainer.SetServiceSelf(_spawner);
        }
    }
}