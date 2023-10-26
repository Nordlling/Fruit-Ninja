using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Splashing;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Infrastructure.Factory
{
    public class SliceEffectFactory : ISliceEffectFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly BlocksConfig _blocksConfig;

        public SliceEffectFactory(ServiceContainer serviceContainer, BlocksConfig blocksConfig)
        {
            _serviceContainer = serviceContainer;
            _blocksConfig = blocksConfig;
        }

        public Splash CreateBlockSplash(Vector2 position, int visualIndex)
        {
            BlockInfo blockInfo = _blocksConfig.Block;
            Splash splash = Object.Instantiate(blockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_serviceContainer.Get<ITimeProvider>(), blockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBombSplash(Vector2 position, int visualIndex)
        {
            BlockInfo blockInfo = _blocksConfig.BombConfig.BlockInfo;
            Splash splash = Object.Instantiate(blockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_serviceContainer.Get<ITimeProvider>(), blockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBonusLifeSplash(Vector2 position, int visualIndex)
        {
            BlockInfo blockInfo = _blocksConfig.BonusLifeConfig.BlockInfo;
            Splash splash = Object.Instantiate(blockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_serviceContainer.Get<ITimeProvider>(), blockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateBlockBagSplash(Vector2 position, int visualIndex)
        {
            BlockInfo blockInfo = _blocksConfig.BlockBagConfig.BlockInfo;
            Splash splash = Object.Instantiate(blockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_serviceContainer.Get<ITimeProvider>(), blockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateFreezeSplash(Vector2 position, int visualIndex)
        {
            BlockInfo blockInfo = _blocksConfig.FreezeConfig.BlockInfo;
            Splash splash = Object.Instantiate(blockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_serviceContainer.Get<ITimeProvider>(), blockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public Splash CreateMagnetSplash(Vector2 position, int visualIndex)
        {
            BlockInfo blockInfo = _blocksConfig.MagnetConfig.BlockInfo;
            Splash splash = Object.Instantiate(blockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_serviceContainer.Get<ITimeProvider>(), blockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
        
        public MagnetAreaEffect CreateMagnetAreaEffect(Vector2 position)
        {
            MagnetConfig magnetConfig = _blocksConfig.MagnetConfig; 
            MagnetAreaEffect magnetAreaEffect = Object.Instantiate(magnetConfig.MagnetAreaEffectPrefab, position, Quaternion.identity);
            magnetAreaEffect.Construct(_serviceContainer.Get<ITimeProvider>(), magnetConfig.AttractionRadius, magnetConfig.Duration);
            return magnetAreaEffect;
        }

        public void Cleanup()
        {
        }
    }
}