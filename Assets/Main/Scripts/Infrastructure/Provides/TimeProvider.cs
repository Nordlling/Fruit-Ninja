using UnityEngine;

namespace Main.Scripts.Infrastructure.Provides
{
    public interface ITimeProvider
    {
        float GetDeltaTime();
        float GetTimeScale();
    }

    public class SlowedTimeProvider : ITimeProvider
    {
        public float TimeScale = 1f;
        public float GetDeltaTime()
        {
            return Time.deltaTime * TimeScale;
        }

        public float GetTimeScale()
        {
            return Time.timeScale * TimeScale;
        }
    }
}