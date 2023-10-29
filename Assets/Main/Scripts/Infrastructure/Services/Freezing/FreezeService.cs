using System;
using System.Threading;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Blurring;
using Main.Scripts.Infrastructure.Services.Boosters;
using Main.Scripts.Logic.Blocks.Freezes;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Freezing
{
    public class FreezeService : IFreezeService, ILoseable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly FreezeConfig _freezeConfig;
        private readonly IBoostersCheckerService _boostersCheckerService;
        private readonly IBlurService _blurService;
        private CancellationTokenSource _cancelToken = new();

        public FreezeService(ITimeProvider timeProvider, FreezeConfig freezeConfig,
            IBoostersCheckerService boostersCheckerService, IBlurService blurService)
        {
            _timeProvider = timeProvider;
            _freezeConfig = freezeConfig;
            _boostersCheckerService = boostersCheckerService;
            _blurService = blurService;
        }

        public event Action OnFreezed;
        public event Action OnUnfreezed;

        public void Freeze()
        {
            _blurService.BlurAll();
            _boostersCheckerService.SetActivation(typeof(Freeze), true);
            _timeProvider.SlowTime(_freezeConfig.TimeScale);

            _cancelToken.Cancel();
            _cancelToken = new();
            FreezeAsync(_freezeConfig.Duration);
            OnFreezed?.Invoke();
        }

        public void Lose()
        {
            _cancelToken.Cancel();
            Unfreeze();
        }

        private async void FreezeAsync(float duration)
        {
            while (duration > 0)
            {
                if (_cancelToken.Token.IsCancellationRequested)
                {
                    break;
                }

                if (!_timeProvider.Stopped)
                {
                    duration -= Time.deltaTime;
                }

                await Task.Yield();
            }

            Unfreeze();
        }

        private void Unfreeze()
        {
            _blurService.UnblurAll();
            _timeProvider.SetRealTime();
            _boostersCheckerService.SetActivation(typeof(Freeze), false);
            OnUnfreezed?.Invoke();
        }
    }
}