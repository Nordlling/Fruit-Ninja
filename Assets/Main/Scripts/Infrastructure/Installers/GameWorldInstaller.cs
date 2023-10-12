using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Logic.Spawn;
using Main.Scripts.Logic.Swipe;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [SerializeField] private DifficultyConfig _difficultyConfig;
        [SerializeField] private BlockConfig _blockConfig;
        [SerializeField] private LivingZone _livingZone;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Camera _camera;
        [SerializeField] private Swiper _swiperPrefab;
        [SerializeField] private CollisionService _collisionServicePrefab;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterLivingZone(serviceContainer);
            RegisterDifficultyService(serviceContainer);
            RegisterSwiper(serviceContainer);
            
            RegisterCollisionService(serviceContainer);
            RegisterGameFactory(serviceContainer);
            RegisterSpawner(serviceContainer);
        }

        private void RegisterLivingZone(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(_livingZone);
        }

        private void RegisterDifficultyService(ServiceContainer serviceContainer)
        {
            DifficultyLevel difficultyLevel = new DifficultyLevel()
            {
                BlockCount = _difficultyConfig.InitialBlockCount,
                Frequency = _difficultyConfig.InitialFrequency
            };
            DifficultyService difficultyService = new DifficultyService(_difficultyConfig, difficultyLevel);
            serviceContainer.SetService<IDifficultyService, DifficultyService>(difficultyService);
        }

        private void RegisterSwiper(ServiceContainer serviceContainer)
        {
            Swiper swiper = Instantiate(_swiperPrefab);
            swiper.Construct(_camera);
            serviceContainer.SetService<ISwiper, Swiper>(swiper);
        }

        private void RegisterCollisionService(ServiceContainer serviceContainer)
        {
            CollisionService collisionService = Instantiate(_collisionServicePrefab);
            collisionService.Construct(serviceContainer.Get<ISwiper>());
            serviceContainer.SetService<ICollisionService, CollisionService>(collisionService);
        }

        private void RegisterGameFactory(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<IGameFactory, GameFactory>(
                new GameFactory(serviceContainer.Get<ICollisionService>(),serviceContainer.Get<LivingZone>(), _blockConfig));
        }

        private void RegisterSpawner(ServiceContainer serviceContainer)
        {
            _spawner.Construct(serviceContainer.Get<IDifficultyService>(),
                serviceContainer.Get<IGameFactory>());
            serviceContainer.SetServiceSelf(_spawner);
        }
    }
}