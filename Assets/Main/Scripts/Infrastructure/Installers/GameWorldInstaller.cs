using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Combo;
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
        [SerializeField] private ComboLabel _comboLabelPrefab;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameplayStateMachine(serviceContainer);
            RegisterBlockContainerService(serviceContainer);

            RegisterLivingZone(serviceContainer);
            RegisterScoreService(serviceContainer);
            RegisterSwiper(serviceContainer);

            RegisterDifficultyService(serviceContainer);
            RegisterCollisionService(serviceContainer);
            RegisterHealthService(serviceContainer);
            RegisterLabelFactory(serviceContainer);
            RegisterComboService(serviceContainer);
            RegisterGameFactory(serviceContainer);
            RegisterSpawner(serviceContainer);
        }

        private void RegisterGameplayStateMachine(ServiceContainer serviceContainer)
        {
            SlowedTimeProvider slowedTimeProvider = RegisterTimeProvider(serviceContainer);

            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine();
            
            gameplayStateMachine.AddState(new PlayState());
            gameplayStateMachine.AddState(new PauseState(slowedTimeProvider));
            gameplayStateMachine.AddState(new LoseState());
            gameplayStateMachine.AddState(new GameOverState());
            gameplayStateMachine.AddState(new RestartState());
            
            serviceContainer.SetService<IGameplayStateMachine, GameplayStateMachine>(gameplayStateMachine);
        }

        private SlowedTimeProvider RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            SlowedTimeProvider slowedTimeProvider = new SlowedTimeProvider();
            serviceContainer.SetService<ITimeProvider, SlowedTimeProvider>(slowedTimeProvider);
            return slowedTimeProvider;
        }

        private void RegisterBlockContainerService(ServiceContainer serviceContainer)
        {
            BlockContainerService blockContainerService =
                new BlockContainerService(serviceContainer.Get<IGameplayStateMachine>());
            
            serviceContainer.SetService<IBlockContainerService, BlockContainerService>(blockContainerService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(blockContainerService);
        }


        private void RegisterLivingZone(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(_livingZone);
        }

        private void RegisterScoreService(ServiceContainer serviceContainer)
        {
            ScoreService scoreService = new ScoreService(
                _scoreConfig,
                serviceContainer.Get<ISaveLoadService>()
                );
            
            serviceContainer.SetService<IScoreService, ScoreService>(scoreService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(scoreService);
        }

        private void RegisterSwiper(ServiceContainer serviceContainer)
        {
            Swiper swiper = Instantiate(_swiperPrefab);
            swiper.Construct(_camera, serviceContainer.Get<ITimeProvider>());
            serviceContainer.SetService<ISwiper, Swiper>(swiper);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(swiper);
        }

        
        private void RegisterDifficultyService(ServiceContainer serviceContainer)
        {
            DifficultyService difficultyService = new DifficultyService(_difficultyConfig);
            serviceContainer.SetService<IDifficultyService, DifficultyService>(difficultyService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(difficultyService);
        }


        private void RegisterCollisionService(ServiceContainer serviceContainer)
        {
            CollisionService collisionService = Instantiate(_collisionServicePrefab);
            collisionService.Construct(
                serviceContainer.Get<ISwiper>(),
                serviceContainer.Get<IBlockContainerService>());
            
            serviceContainer.SetService<ICollisionService, CollisionService>(collisionService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(collisionService);
        }

        private void RegisterHealthService(ServiceContainer serviceContainer)
        {
            HealthService healthService = new HealthService(
                _healthConfig,
                serviceContainer.Get<IGameplayStateMachine>());
            
            serviceContainer.SetService<IHealthService, HealthService>(healthService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(healthService);
        }
        
        private void RegisterLabelFactory(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<ILabelFactory, LabelFactory>(
                new LabelFactory(
                    serviceContainer.Get<ITimeProvider>(),
                    serviceContainer.Get<LivingZone>())
            );
        }

        private void RegisterComboService(ServiceContainer serviceContainer)
        {
            ComboService comboService = new ComboService(
                _scoreConfig,
                serviceContainer.Get<IScoreService>(),
                serviceContainer.Get<ILabelFactory>(),
                _comboLabelPrefab);

            serviceContainer.SetService<IComboService, ComboService>(comboService);
        }

        private void RegisterGameFactory(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<IGameFactory, GameFactory>(
                new GameFactory(
                    serviceContainer.Get<IBlockContainerService>(),
                    serviceContainer.Get<LivingZone>(), 
                    serviceContainer.Get<IHealthService>(),
                    serviceContainer.Get<IScoreService>(),
                    serviceContainer.Get<IComboService>(),
                    serviceContainer.Get<ITimeProvider>(),
                    serviceContainer.Get<ILabelFactory>(),
                    _blockConfig)
                );
        }

        private void RegisterSpawner(ServiceContainer serviceContainer)
        {
            _spawner.Construct(
                serviceContainer.Get<IDifficultyService>(),
                serviceContainer.Get<IGameFactory>(),
                serviceContainer.Get<ITimeProvider>());
            
            serviceContainer.SetServiceSelf(_spawner);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(_spawner);
        }
    }
}