using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.AnimationTargetContainer;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Boosters;
using Main.Scripts.Infrastructure.Services.Bricking;
using Main.Scripts.Infrastructure.Services.ButtonContainer;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Explosion;
using Main.Scripts.Infrastructure.Services.Freezing;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Infrastructure.Services.Magnetism;
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
        [Header("Configs")]
        [SerializeField] private DifficultyConfig _difficultyConfig;
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private WordEndingsConfig _wordEndingsConfig;
        [SerializeField] private BlocksConfig _blocksConfig;
        
        [Header("Prefabs")]
        [SerializeField] private Swiper _swiperPrefab;
        [SerializeField] private CollisionService _collisionServicePrefab;
        [SerializeField] private ComboLabel _comboLabelPrefab;
        [SerializeField] private SpawnArea _spawnAreaPrefab;
        
        [Header("Scene Objects")]
        [SerializeField] private Camera _camera;
        [SerializeField] private LivingZone _livingZone;
        [SerializeField] private Spawner _spawner;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterTimeProvider(serviceContainer);
            RegisterGameplayStateMachine(serviceContainer);
            RegisterBlockContainerService(serviceContainer);
            RegisterAnimationTargetContainer(serviceContainer);
            
            RegisterLabelFactory(serviceContainer);
            RegisterSpawnFactory(serviceContainer);
            RegisterSliceEffectFactory(serviceContainer);
            RegisterBlockFactory(serviceContainer);

            RegisterLivingZone(serviceContainer);
            RegisterSwiper(serviceContainer);
            RegisterCollisionService(serviceContainer);
            
            RegisterScoreService(serviceContainer);
            RegisterDifficultyService(serviceContainer);
            RegisterHealthService(serviceContainer);
            RegisterComboService(serviceContainer);

            RegisterBoostersCheckerService(serviceContainer);
            RegisterExplosionService(serviceContainer);
            RegisterFreezeService(serviceContainer);
            RegisterMagnetService(serviceContainer);
            RegisterBrickService(serviceContainer);
            
            RegisterSpawner(serviceContainer);
        }


        private void RegisterTimeProvider(ServiceContainer serviceContainer)
        {
            SlowedTimeProvider slowedTimeProvider = new SlowedTimeProvider();
            
            serviceContainer.SetService<ITimeProvider, SlowedTimeProvider>(slowedTimeProvider);
        }

        private void RegisterGameplayStateMachine(ServiceContainer serviceContainer)
        {
            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine();
            
            gameplayStateMachine.AddState(new PlayState());
            gameplayStateMachine.AddState(new PauseState(serviceContainer.Get<ITimeProvider>()));
            gameplayStateMachine.AddState(new LoseState(serviceContainer.Get<IButtonContainerService>()));
            gameplayStateMachine.AddState(new GameOverState());
            gameplayStateMachine.AddState(new RestartState());
            gameplayStateMachine.AddState(new PrepareState(serviceContainer.Get<IButtonContainerService>()));
            
            serviceContainer.SetService<IGameplayStateMachine, GameplayStateMachine>(gameplayStateMachine);
            gameplayStateMachine.Enter<PlayState>();
        }

        private void RegisterBlockContainerService(ServiceContainer serviceContainer)
        {
            BlockContainerService blockContainerService = new BlockContainerService(serviceContainer.Get<IGameplayStateMachine>());
            
            serviceContainer.SetService<IBlockContainerService, BlockContainerService>(blockContainerService);
            
            SetGameplayStates(serviceContainer, blockContainerService);
        }

        private void RegisterAnimationTargetContainer(ServiceContainer serviceContainer)
        {
            AnimationTargetContainer animationTargetContainer = new AnimationTargetContainer();
            
            serviceContainer.SetService<IAnimationTargetContainer, AnimationTargetContainer>(animationTargetContainer);
        }


        private void RegisterSpawnFactory(ServiceContainer serviceContainer)
        {
            SpawnFactory spawnFactory = new SpawnFactory(_spawnAreaPrefab);
            serviceContainer.SetService<ISpawnFactory, SpawnFactory>(spawnFactory);
        }

        private void RegisterLabelFactory(ServiceContainer serviceContainer)
        {
            LabelFactory labelFactory = new LabelFactory(serviceContainer, _wordEndingsConfig);
            serviceContainer.SetService<ILabelFactory, LabelFactory>(labelFactory);
        }

        private void RegisterSliceEffectFactory(ServiceContainer serviceContainer)
        {
            SliceEffectFactory sliceEffectFactory = new SliceEffectFactory(serviceContainer, _blocksConfig);
            serviceContainer.SetService<ISliceEffectFactory, SliceEffectFactory>(sliceEffectFactory);
        }

        private void RegisterBlockFactory(ServiceContainer serviceContainer)
        {
            BlockFactory blockFactory = new BlockFactory(serviceContainer,_blocksConfig);
            serviceContainer.SetService<IBlockFactory, BlockFactory>(blockFactory);
        }
        

        private void RegisterLivingZone(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(_livingZone);
        }

        private void RegisterSwiper(ServiceContainer serviceContainer)
        {
            Swiper swiper = Instantiate(_swiperPrefab);
            swiper.Construct(_camera, serviceContainer.Get<ITimeProvider>());
            serviceContainer.SetService<ISwiper, Swiper>(swiper);

            SetGameplayStates(serviceContainer, swiper);
        }

        private void RegisterCollisionService(ServiceContainer serviceContainer)
        {
            CollisionService collisionService = Instantiate(_collisionServicePrefab);
            collisionService.Construct(serviceContainer.Get<ISwiper>(), serviceContainer.Get<IBlockContainerService>());
            
            serviceContainer.SetService<ICollisionService, CollisionService>(collisionService);
            
            SetGameplayStates(serviceContainer, collisionService);
        }

        private void RegisterScoreService(ServiceContainer serviceContainer)
        {
            ScoreService scoreService = new ScoreService(_scoreConfig, serviceContainer.Get<ISaveLoadService>());
            
            serviceContainer.SetService<IScoreService, ScoreService>(scoreService);
            
            SetGameplayStates(serviceContainer, scoreService);
        }


        private void RegisterDifficultyService(ServiceContainer serviceContainer)
        {
            DifficultyService difficultyService = new DifficultyService(_difficultyConfig);
            
            serviceContainer.SetService<IDifficultyService, DifficultyService>(difficultyService);
            
            SetGameplayStates(serviceContainer, difficultyService);
        }

        private void RegisterHealthService(ServiceContainer serviceContainer)
        {
            HealthService healthService = new HealthService(_healthConfig, serviceContainer.Get<IGameplayStateMachine>());
            
            serviceContainer.SetService<IHealthService, HealthService>(healthService);
            
            SetGameplayStates(serviceContainer, healthService);
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

        private void RegisterBoostersCheckerService(ServiceContainer serviceContainer)
        {
            BoostersCheckerService boostersCheckerService = new BoostersCheckerService
            (
                _blocksConfig,
                serviceContainer.Get<IBlockContainerService>(),
                serviceContainer.Get<IHealthService>()
            );
            
            serviceContainer.SetService<IBoostersCheckerService, BoostersCheckerService>(boostersCheckerService);
        }

        private void RegisterExplosionService(ServiceContainer serviceContainer)
        {
            ExplosionService explosionService = new ExplosionService(serviceContainer.Get<IBlockContainerService>(), _blocksConfig.BombConfig);
            serviceContainer.SetService<IExplosionService, ExplosionService>(explosionService);
        }

        private void RegisterFreezeService(ServiceContainer serviceContainer)
        {
            FreezeService freezeService = new FreezeService
            (
                serviceContainer.Get<ITimeProvider>(),
                _blocksConfig.FreezeConfig,
                serviceContainer.Get<IBoostersCheckerService>()
            );
            
            serviceContainer.SetService<IFreezeService, FreezeService>(freezeService);
            
            SetGameplayStates(serviceContainer, freezeService);
        }

        private void RegisterMagnetService(ServiceContainer serviceContainer)
        {
            MagnetService magnetService = new MagnetService
            (
                serviceContainer.Get<IBoostersCheckerService>(),
                serviceContainer.Get<IBlockContainerService>(), 
                serviceContainer.Get<ITimeProvider>(),
                _blocksConfig.MagnetConfig
            );
            
            serviceContainer.SetService<IMagnetService, MagnetService>(magnetService);
        }
        
        private void RegisterBrickService(ServiceContainer serviceContainer)
        {
            BrickService brickService = new BrickService(serviceContainer.Get<ISwiper>());
            
            serviceContainer.SetService<IBrickService, BrickService>(brickService);
        }

        private void RegisterSpawner(ServiceContainer serviceContainer)
        {
            _spawner.Construct
            (
                serviceContainer.Get<IDifficultyService>(),
                serviceContainer.Get<IBlockFactory>(),
                serviceContainer.Get<ISpawnFactory>(),
                serviceContainer.Get<ITimeProvider>(),
                serviceContainer.Get<IBoostersCheckerService>()
            );
            
            serviceContainer.SetServiceSelf(_spawner);
            
            SetGameplayStates(serviceContainer, _spawner);
        }

        private void SetGameplayStates(ServiceContainer serviceContainer, IGameplayStatable gameplayStatable)
        {
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameplayStatable);
        }
    }
}