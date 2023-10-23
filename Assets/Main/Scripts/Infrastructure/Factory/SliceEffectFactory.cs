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

        public SliceEffectFactory(ITimeProvider timeProvider, BlockTypesConfig blockTypesConfig)
        {
            _timeProvider = timeProvider;
            _blockTypesConfig = blockTypesConfig;
        }

        public Splash CreateBlockSplash(Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(_blockTypesConfig.Block.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider, _blockTypesConfig.Block.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBombSplash(Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(_blockTypesConfig.Bomb.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider, _blockTypesConfig.Bomb.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBonusLifeSplash(Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(_blockTypesConfig.BonusLife.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider, _blockTypesConfig.BonusLife.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }

        public void Cleanup()
        {
        }
    }
}