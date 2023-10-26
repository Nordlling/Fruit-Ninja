using System;
using System.Threading;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Boosters;
using Main.Scripts.Infrastructure.Services.Health;

namespace Main.Scripts.Infrastructure.Services.Samuraism
{
    public class SamuraiService : ISamuraiService
    {
        private readonly IBoostersCheckerService _boostersCheckerService;
        private readonly IHealthService _healthService;
        private readonly ITimeProvider _timeProvider;
        private readonly SamuraiConfig _samuraiConfig;

        public SamuraiInfo SamuraiInfo { get; private set; } = new();
        public bool Activated { get; private set; }
        
        public event Action OnStarted;
        public event Action<float> OnTimerUpdated;
        public event Action OnFinished;

        private CancellationTokenSource _cancelToken = new();


        public SamuraiService(
            IBoostersCheckerService boostersCheckerService, 
            IHealthService healthService, 
            ITimeProvider timeProvider, 
            SamuraiConfig samuraiConfig)
        {
            _boostersCheckerService = boostersCheckerService;
            _healthService = healthService;
            _timeProvider = timeProvider;
            _samuraiConfig = samuraiConfig;
        }

        public void ActivateSamurai()
        {
            _boostersCheckerService.SetActivation(_samuraiConfig.BlockInfo.BlockPrefab.GetType(), true);
            _cancelToken.Cancel();
            _cancelToken = new();

            SamuraiInfo.BlockCountMultiplier = _samuraiConfig.BlockCountMultiplier;
            SamuraiInfo.PackFrequencyMultiplier = _samuraiConfig.PackFrequencyMultiplier;
            SamuraiInfo.BlockFrequencyMultiplier = _samuraiConfig.BlockFrequencyMultiplier;
            Activated = true;
            OnStarted?.Invoke();
            
            _healthService.SwitchBlock(true);
            
            ActivateSamuraiAsync(_samuraiConfig.Duration);
        }

        private async void ActivateSamuraiAsync(float duration)
        {
            while (duration > 0)
            {
                if (_cancelToken.Token.IsCancellationRequested)
                {
                    break;
                }

                if (!_timeProvider.Stopped)
                {
                    duration -= _timeProvider.GetDeltaTime();
                    OnTimerUpdated?.Invoke(duration);
                }

                await Task.Yield();
            }
            DeactivateSamurai();
        }

        private void DeactivateSamurai()
        {
            OnFinished?.Invoke();
            SamuraiInfo.ResetValues();
            _healthService.SwitchBlock(false);
            Activated = false;
            _boostersCheckerService.SetActivation(_samuraiConfig.BlockInfo.BlockPrefab.GetType(), false);
        }
    }
}