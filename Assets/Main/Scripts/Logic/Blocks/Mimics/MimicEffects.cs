using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Mimics
{
    public class MimicEffects : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _mimicEffects;
        [SerializeField] private ParticleSystem[] _prepareEffects;
        
        private ITimeProvider _timeProvider;

        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void PlayPrepareEffects()
        {
            for (int i = 0; i < _prepareEffects.Length; i++)
            {
                _prepareEffects[i].gameObject.SetActive(true);
            }
        }

        public void StopPrepareEffects()
        {
            for (int i = 0; i < _prepareEffects.Length; i++)
            {
                _prepareEffects[i].gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            UpdateTimeForEffects();
        }

        private void UpdateTimeForEffects()
        {
            for (int i = 0; i < _prepareEffects.Length; i++)
            {
                var splashEffectMain = _prepareEffects[i].main;
                splashEffectMain.simulationSpeed = _timeProvider.Stopped ? 0f : 1f;
            }

            for (int i = 0; i < _mimicEffects.Length; i++)
            {
                var splashEffectMain = _mimicEffects[i].main;
                splashEffectMain.simulationSpeed = _timeProvider.Stopped ? 0f : 1f;
            }
        }
    }
}