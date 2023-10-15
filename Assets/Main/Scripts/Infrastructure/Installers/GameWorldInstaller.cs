using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Spawn;
using Main.Scripts.Logic.Swipe;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameWorldInstaller : MonoInstaller
    {
        [SerializeField] private DifficultyConfig _difficultyConfig;
        [SerializeField] private BlockConfig _blockConfig;
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private LivingZone _livingZone;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Camera _camera;
        [SerializeField] private Swiper _swiperPrefab;
        [SerializeField] private CollisionService _collisionServicePrefab;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterSaveLoadService(serviceContainer);
            RegisterLivingZone(serviceContainer);
            RegisterDifficultyService(serviceContainer);
            RegisterHealthService(serviceContainer);
            RegisterScoreService(serviceContainer);
            RegisterSwiper(serviceContainer);

            RegisterCollisionService(serviceContainer);
            RegisterGameFactory(serviceContainer);
            RegisterSpawner(serviceContainer);
        }

        private static void RegisterSaveLoadService(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<ISaveLoadService, SaveLoadService>(new SaveLoadService());
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

        private void RegisterHealthService(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<IHealthService, HealthService>(new HealthService(_healthConfig));
        }

        private void RegisterScoreService(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<IScoreService, ScoreService>(new ScoreService(_scoreConfig, serviceContainer.Get<ISaveLoadService>()));
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
                new GameFactory(
                    serviceContainer.Get<ICollisionService>(),
                    serviceContainer.Get<LivingZone>(), 
                    serviceContainer.Get<IHealthService>(),
                    serviceContainer.Get<IScoreService>(),
                    _blockConfig));
        }

        private void RegisterSpawner(ServiceContainer serviceContainer)
        {
            _spawner.Construct(
                serviceContainer.Get<IDifficultyService>(),
                serviceContainer.Get<IGameFactory>(),
                serviceContainer.Get<IHealthService>());
            
            serviceContainer.SetServiceSelf(_spawner);
        }
    }
}