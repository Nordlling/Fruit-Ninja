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
        private readonly BlocksConfig _blocksConfig;

        public SliceEffectFactory(ITimeProvider timeProvider, BlocksConfig blocksConfig)
        {
            _timeProvider = timeProvider;
            _blocksConfig = blocksConfig;
        }

        public Splash CreateBlockSplash(Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(_blocksConfig.Block.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider, _blocksConfig.Block.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBombSplash(Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(_blocksConfig.BombConfig.BlockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider, _blocksConfig.BombConfig.BlockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBonusLifeSplash(Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(_blocksConfig.BonusLifeConfig.BlockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider, _blocksConfig.BonusLifeConfig.BlockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBlockBagSplash(Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(_blocksConfig.BlockBagConfig.BlockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_timeProvider, _blocksConfig.BlockBagConfig.BlockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }

        public void Cleanup()
        {
        }
    }
}