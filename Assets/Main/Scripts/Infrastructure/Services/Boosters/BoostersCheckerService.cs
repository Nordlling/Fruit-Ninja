using System.Collections.Generic;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Logic.Spawn;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Boosters
{
    public class BoostersCheckerService : IBoostersCheckerService
    {
        private readonly BoostersConfig _boostersConfig;
        private readonly IBlockContainerService _blockContainerService;
        private readonly IHealthService _healthService;
        
        private readonly Dictionary<BoosterType, int> _boosterCounters = new();

        public BoostersCheckerService(BoostersConfig boostersConfig, IBlockContainerService blockContainerService, IHealthService healthService)
        {
            _boostersConfig = boostersConfig;
            _blockContainerService = blockContainerService;
            _healthService = healthService;
        }
        
        public void CalculateBlockMaxCounter(int packBlockCount)
        {
            foreach (BoosterInfo boosterInfo in _boostersConfig.Boosters)
            {
                BoosterSpawnInfo boosterSpawnInfo = boosterInfo.BoosterSpawnInfo;
                
                int blockMaxCounter = (int)(packBlockCount * boosterSpawnInfo.MaxFractionInBoostPack);

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

                _boosterCounters[boosterInfo.BoosterType] = blockMaxCounter;
            }
        }
        
        public bool TrySpawnBooster(SpawnArea spawnArea, BoosterInfo boosterInfo)
        {
            switch (boosterInfo.BoosterType)
            {
                case BoosterType.Bomb:
                    return TrySpawnBomb(boosterInfo, spawnArea);
                case BoosterType.BonusLife:
                    return TrySpawnBonusLife(boosterInfo, spawnArea);
                case BoosterType.BlockBug:
                    Debug.Log($"Spawn {boosterInfo.BoosterType}");
                    return true;
                case BoosterType.Freeze:
                    Debug.Log($"Spawn {boosterInfo.BoosterType}");
                    return true;
                case BoosterType.Magnet:
                    Debug.Log($"Spawn {boosterInfo.BoosterType}");
                    return true;
                case BoosterType.Brick:
                    Debug.Log($"Spawn {boosterInfo.BoosterType}");
                    return true;
                case BoosterType.Samurai:
                    Debug.Log($"Spawn {boosterInfo.BoosterType}");
                    return true;
                case BoosterType.Mimic:
                    Debug.Log($"Spawn {boosterInfo.BoosterType}");
                    return true;
            }

            return false;
        }

        private bool TrySpawnBomb(BoosterInfo boosterInfo, SpawnArea spawnArea)
        {
            if (_boosterCounters[boosterInfo.BoosterType] <= 0)
            {
                return false;
            }
            if (boosterInfo.BoosterSpawnInfo.MaxNumberOnScreen != -1 && _blockContainerService.BonusLifes.Count >= boosterInfo.BoosterSpawnInfo.MaxNumberOnScreen)
            {
                return false;
            }
            
            spawnArea.SpawnBomb();
            _boosterCounters[boosterInfo.BoosterType]--;
            return true;
        }
        
        private bool TrySpawnBonusLife(BoosterInfo boosterInfo, SpawnArea spawnArea)
        {
            if (boosterInfo.BoosterSpawnInfo.MaxNumberOnScreen != -1 && _blockContainerService.BonusLifes.Count >= boosterInfo.BoosterSpawnInfo.MaxNumberOnScreen)
            {
                return false;
            }
            
            if (_healthService.IsMaxHealth())
            {
                return false;
            }
            
            spawnArea.SpawnBonusLife();
            return true;
        }
    }
}