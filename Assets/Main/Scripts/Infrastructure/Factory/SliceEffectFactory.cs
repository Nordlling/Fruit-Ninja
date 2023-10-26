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
            return CreateSplash(_blocksConfig.Block, position, visualIndex);
        }

        public Splash CreateBombSplash(Vector2 position, int visualIndex)
        {
            return CreateSplash(_blocksConfig.BombConfig.BlockInfo, position, visualIndex);
        }

        public Splash CreateBonusLifeSplash(Vector2 position, int visualIndex)
        {
            return CreateSplash(_blocksConfig.BonusLifeConfig.BlockInfo, position, visualIndex);
        }

        public Splash CreateBlockBagSplash(Vector2 position, int visualIndex)
        {
            return CreateSplash(_blocksConfig.BlockBagConfig.BlockInfo, position, visualIndex);
        }

        public Splash CreateFreezeSplash(Vector2 position, int visualIndex)
        {
            return CreateSplash(_blocksConfig.FreezeConfig.BlockInfo, position, visualIndex);
        }

        public Splash CreateMagnetSplash(Vector2 position, int visualIndex)
        {
            return CreateSplash(_blocksConfig.MagnetConfig.BlockInfo, position, visualIndex);
        }

        public MagnetAreaEffect CreateMagnetAreaEffect(Vector2 position)
        {
            MagnetConfig magnetConfig = _blocksConfig.MagnetConfig; 
            MagnetAreaEffect magnetAreaEffect = Object.Instantiate(magnetConfig.MagnetAreaEffectPrefab, position, Quaternion.identity);
            magnetAreaEffect.Construct(_serviceContainer.Get<ITimeProvider>(), magnetConfig.AttractionRadius, magnetConfig.Duration);
            return magnetAreaEffect;
        }

        public Splash CreateBrickSplash(Vector2 position, int visualIndex)
        {
            return CreateSplash(_blocksConfig.BrickConfig.BlockInfo, position, visualIndex);
        }

        public Splash CreateSamuraiSplash(Vector2 position, int visualIndex)
        {
            return CreateSplash(_blocksConfig.SamuraiConfig.BlockInfo, position, visualIndex);
        }

        public void Cleanup()
        {
        }

        private Splash CreateSplash(BlockInfo blockInfo, Vector2 position, int visualIndex)
        {
            Splash splash = Object.Instantiate(blockInfo.SplashPrefab, position, Quaternion.identity);
            splash.Construct(_serviceContainer.Get<ITimeProvider>(), blockInfo.VisualSprites[visualIndex].SplashSprite);
            return splash;
        }
    }
}