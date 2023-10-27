using System;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Mimicring;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Mimics
{
    public class MimicSwitcher : MonoBehaviour
    {
        [SerializeField] private MimicEffects _mimicEffects;
        
        private IMimicService _mimicService;
        private ITimeProvider _timeProvider;
        private MimicConfig _mimicConfig;

        private float _leftTimeToSwitch;
        private float _prepareTime;
        private BlockPiece _block;

        public void Construct(IMimicService mimicService, ITimeProvider timeProvider, MimicConfig mimicConfig)
        {
            _mimicService = mimicService;
            _timeProvider = timeProvider;
            _mimicConfig = mimicConfig;
            
            _leftTimeToSwitch = _mimicConfig.TransformationTime;
            _prepareTime = _mimicConfig.PrepareTime;
            
            _mimicEffects.Construct(timeProvider);
        }

        private void Start()
        {
            SwitchToRandomBlock(null);
        }

        private void Update()
        {
            if (_block == null)
            {
                Destroy(gameObject);
                return;
            }
            
            if (_leftTimeToSwitch <= 0)
            {
                _mimicEffects.StopPrepareEffects();
                Destroy(_block.gameObject);
                SwitchToRandomBlock(_block.GetType());
                _leftTimeToSwitch = _mimicConfig.TransformationTime;
                return;
            }
            
            if (_leftTimeToSwitch <= _prepareTime)
            {
                _mimicEffects.PlayPrepareEffects();
            }

            _leftTimeToSwitch -= _timeProvider.GetDeltaTime();
        }

        private void SwitchToRandomBlock(Type type)
        {
            BlockPiece block = _mimicService.GetRandomMimicBlock(type, transform.position);
            _block = block;
            _block.transform.SetParent(transform);
        }
    }
}