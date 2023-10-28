using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Main.Scripts.Constants;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using Main.Scripts.Infrastructure.Services.Boosters;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Magnetism
{
    public class MagnetService : IMagnetService, ILoseable
    {
        private readonly IBoostersCheckerService _boostersCheckerService;
        private readonly IBlockContainerService _blockContainerService;
        private readonly ITimeProvider _timeProvider;
        private readonly MagnetConfig _magnetConfig;

        private readonly List<Type> _typesToMagnet = new();
        private readonly List<BlockPiece> _attractedBlocks = new();
        private readonly List<CancellationTokenSource> _cancelTokens = new();

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
            
            foreach (BlockType blockType in magnetConfig.TypesToMagnet)
            {
                _typesToMagnet.Add(BlockTypesConstants.BlockTypes[blockType]);
            }
        }

        public void Attract(Vector2 attractPosition)
        {
            _boostersCheckerService.SetActivation(_magnetConfig.BlockInfo.BlockPrefab.GetType(), true);

            CancellationTokenSource cancelToken = new();
            _cancelTokens.Add(cancelToken);
            _attractedBlocks.Clear();
            AttractAsync(_magnetConfig.Duration, attractPosition, cancelToken);
        }

        public void Lose()
        {
            _cancelTokens.ForEach(token => token.Cancel());
            Unattract();
        }

        private async void AttractAsync(float duration, Vector2 attractionPosition, CancellationTokenSource cancelToken)
        {
            while (duration > 0)
            {
                if (cancelToken.Token.IsCancellationRequested)
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

                if (block.BlockMovement == null)
                {
                    continue;
                }
                
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
                block.BlockAnimation.enabled = false;
                
                _attractedBlocks.Add(block);
            }
        }

        private void Unattract()
        {
            _boostersCheckerService.SetActivation(_magnetConfig.BlockInfo.BlockPrefab.GetType(), false);
        }
    }
}