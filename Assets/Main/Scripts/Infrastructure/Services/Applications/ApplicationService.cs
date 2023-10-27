using System;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Applications
{
    public class ApplicationService : MonoBehaviour, IApplicationService
    {
        private ApplicationConfig _applicationConfig;

        public event Action OnPaused;

        public void Construct(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        private void OnEnable()
        {
            Application.focusChanged += FocusChanged;
        }

        private void OnDisable()
        {
            Application.focusChanged -= FocusChanged;
        }

        private void Start()
        {
            Application.targetFrameRate = _applicationConfig.TargetFPS;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }


        private void FocusChanged(bool isFocused)
        {
            if (!isFocused)
            {
#if !UNITY_EDITOR
                OnPaused?.Invoke();
#endif
            }
        }
    }
}