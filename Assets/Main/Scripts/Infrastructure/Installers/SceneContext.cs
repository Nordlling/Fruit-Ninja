using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts.Infrastructure.Installers
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers = new();

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            BuildContainer();
        }

        private ServiceContainer BuildContainer()
        {
            var container = ServiceContainer.Instance;
            foreach (var installer in _installers) installer.InstallBindings(container);

            return container;
        }
    }
}