using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Boosters;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Magnetism
{
    public class MagnetService : IMagnetService, ILoseable
    {
        private readonly IBoostersCheckerService _boostersCheckerService;
        private readonly IBlockContainerService _blockContainerService;
        private readonly ITimeProvider _timeProvider;
        private readonly MagnetConfig _magnetConfig;

        private readonly List<Type> _typesToMagnet;
        private readonly List<BlockPiece> _attractedBlocks = new();
        
        private CancellationTokenSource _cancelToken = new();

        public MagnetService(
            IBoostersCheckerService boostersCheckerService, 
            IBlockContainerService blockContainerService, 
            ITimeProvider timeProvider, 
            MagnetConfig magnetConfig)
        {
            _boostersCheckerService = boostersCheckerService;
            _blockContainerService = blockContainerService;
            _timeProvider = timeProvider;
            _magnetConfig = magnetConfig;

            _typesToMagnet = new List<Type>
            {
                typeof(BlockPiece),
                typeof(Block),
                typeof(BonusLife),
                typeof(BlockBag),
                typeof(Freeze),
                typeof(Magnet),
            };
        }

        public void Attract(Vector2 attractPosition)
        {
            _boostersCheckerService.SetActivation(_magnetConfig.BlockInfo.BlockPrefab.GetType(), true);

            _cancelToken.Cancel();
            _cancelToken = new();
            _attractedBlocks.Clear();
            AttractAsync(_magnetConfig.Duration, attractPosition);
        }

        public void Lose()
        {
            _cancelToken.Cancel();
            Unattract();
        }

        private async void AttractAsync(float duration, Vector2 attractionPosition)
        {
            while (duration > 0)
            {
                if (_cancelToken.Token.IsCancellationRequested)
                {
                    break;
                }

                if (!_timeProvider.Stopped)
                {
                    ExecuteAttract(attractionPosition, duration);
                    duration -= _timeProvider.GetDeltaTime();
                }

                await Task.Yield();
            }
            Unattract();
        }

        private void ExecuteAttract(Vector2 attractionPosition, float duration)
        {
            for (int i = 0; i < _blockContainerService.BlocksCount; i++)
            {
                BlockPiece block = _blockContainerService.AllBlocks[i];
                
                if (!_typesToMagnet.Contains(block.GetType()) || _attractedBlocks.Contains(block))
                {
                    continue;
                }
                
                float distance = Vector2.Distance(block.transform.position, attractionPosition);
                
                if (distance > _magnetConfig.AttractionRadius)
                {
                    continue;
                }
                
                block.BlockMovement.AddAttraction(attractionPosition, duration, _magnetConfig);
                block.BlockAnimation.StopAnimation();
                
                _attractedBlocks.Add(block);
            }
        }

        private void Unattract()
        {
            _boostersCheckerService.SetActivation(_magnetConfig.BlockInfo.BlockPrefab.GetType(), false);
        }
    }
}