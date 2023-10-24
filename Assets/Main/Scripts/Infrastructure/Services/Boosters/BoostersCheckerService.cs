using System;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Spawn;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Boosters
{
    public class BoostersCheckerService : IBoostersCheckerService
    {
        private readonly BlocksConfig _blocksConfig;
        private readonly IBlockContainerService _blockContainerService;
        private readonly IHealthService _healthService;
        private readonly Dictionary<Type, BoosterInfo> _boosterInfos;

        public List<BoosterConfig> BoosterConfigs { get; private set; }
        public int MaxCountInPack { get; private set; }

        public BoostersCheckerService(BlocksConfig blocksConfig, IBlockContainerService blockContainerService, IHealthService healthService)
        {
            _blocksConfig = blocksConfig;
            _blockContainerService = blockContainerService;
            _healthService = healthService;
            
            _boosterInfos = new Dictionary<Type, BoosterInfo>
            {
                { typeof(Bomb), new BoosterInfo(_blocksConfig.BombConfig.BoosterSpawnInfo, TrySpawnBomb) },
                { typeof(BonusLife), new BoosterInfo(_blocksConfig.BonusLifeConfig.BoosterSpawnInfo, TrySpawnBonusLife) },
                { typeof(BlockBag), new BoosterInfo(_blocksConfig.BlockBagConfig.BoosterSpawnInfo, TrySpawnBlockBag) }
            };

            BoosterConfigs = new List<BoosterConfig>()
            {
                _blocksConfig.BombConfig,
                _blocksConfig.BonusLifeConfig,
                _blocksConfig.BlockBagConfig
            };
        }

        public void CalculateBlockMaxCounter(int packBlockCount)
        {
            MaxCountInPack = (int)(packBlockCount * _blocksConfig.MaxFractionInPack);
            foreach (KeyValuePair<Type, BoosterInfo> kvp in _boosterInfos)
            {
                BoosterSpawnInfo boosterSpawnInfo = kvp.Value.BoosterSpawnInfo;
                int blockMaxCounter = (int)(MaxCountInPack * boosterSpawnInfo.MaxFractionInBoostPack);

                if (blockMaxCounter == 0)
                {
                    blockMaxCounter = 1;
                }

                if (boosterSpawnInfo.MaxNumberInBoostPack != -1)
                {
                    blockMaxCounter = Mathf.Min(blockMaxCounter, boosterSpawnInfo.MaxNumberInBoostPack);
                }

                if (boosterSpawnInfo.MaxNumberOnScreen != -1)
                {
                    blockMaxCounter = Mathf.Min(blockMaxCounter, boosterSpawnInfo.MaxNumberOnScreen);
                }

                _boosterInfos[kvp.Key].Count = blockMaxCounter;
            }
        }
        
        public bool TrySpawnBooster(SpawnArea spawnArea, int index)
        {
            BoosterConfig boosterConfig = BoosterConfigs[index];
            Type currentType = boosterConfig.BlockInfo.BlockPrefab.GetType();
            
            if (_boosterInfos.ContainsKey(currentType))
            {
                return _boosterInfos[currentType].SpawnAction(boosterConfig, spawnArea, currentType);
            }
            
            return false;
        }

        private bool TrySpawnBomb(BoosterConfig boosterConfig, SpawnArea spawnArea, Type type)
        {
            if (!CanSpawn(boosterConfig, type))
            {
                return false;
            }

            spawnArea.SpawnBomb();
            _boosterInfos[type].Count--;
            return true;
        }

        private bool TrySpawnBonusLife(BoosterConfig boosterConfig, SpawnArea spawnArea, Type type)
        {
            if (!CanSpawn(boosterConfig, type))
            {
                return false;
            }
            
            if (_healthService.IsMaxHealth())
            {
                return false;
            }
            
            spawnArea.SpawnBonusLife();
            _boosterInfos[type].Count--;
            return true;
        }
        
        private bool TrySpawnBlockBag(BoosterConfig boosterConfig, SpawnArea spawnArea, Type type)
        {
            if (!CanSpawn(boosterConfig, type))
            {
                return false;
            }
            
            spawnArea.SpawnBlockBag();
            _boosterInfos[type].Count--;
            return true;
        }

        private bool CanSpawn(BoosterConfig boosterConfig, Type type)
        {
            if (_boosterInfos[type].Count <= 0)
            {
                return false;
            }

            if (boosterConfig.BoosterSpawnInfo.MaxNumberOnScreen != -1 &&
                _blockContainerService.BlockTypes[typeof(BonusLife)].Count >= boosterConfig.BoosterSpawnInfo.MaxNumberOnScreen)
            {
                return false;
            }

            return true;
        }
    }
}