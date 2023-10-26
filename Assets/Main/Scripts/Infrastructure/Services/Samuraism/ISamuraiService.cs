using System;

namespace Main.Scripts.Infrastructure.Services.Samuraism
{
    public interface ISamuraiService
    {
        SamuraiInfo SamuraiInfo { get; }
        bool Activated { get; }
        void ActivateSamurai();
        event Action OnStarted;
        event Action OnFinished;
        event Action<float> OnTimerUpdated;
    }
}