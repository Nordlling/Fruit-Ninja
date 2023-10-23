using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Splashing;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Infrastructure.Factory
{
    public class SliceEffectFactory : ISliceEffectFactory
    {
        private readonly ITimeProvider _timeProvider;
        private readonly BlockTypesConfig _blockTypesConfig;
        private readonly Splash _splashPrefab;

        public SliceEffectFactory(ITimeProvider timeProvider, BlockTypesConfig blockTypesConfig, Splash splashPrefab)
        {
            _timeProvider = timeProvider;
            _blockTypesConfig = blockTypesConfig;
            _splashPrefab = splashPrefab;
        }

        public Splash CreateBlockSplash(Vector2 position)
        {
            Splash splash = Object.Instantiate(_splashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider);
            return splash;
        }
        
        public Splash CreateBombSplash(Vector2 position)
        {
            Splash splash = Object.Instantiate(_splashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider);
            return splash;
        }

        public void Cleanup()
        {
        }
    }
}