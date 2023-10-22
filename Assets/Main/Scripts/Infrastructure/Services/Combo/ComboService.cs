using System;
using System.Threading;
using System.Threading.Tasks;
using Main.Scripts.Data;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Combo;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Combo
{
    public class ComboService : IComboService
    {
        public Vector2 CurrentPosition { get; private set; }

        private readonly ScoreConfig _scoreConfig;
        private readonly IScoreService _scoreService;
        private readonly ILabelFactory _labelFactory;
        private readonly ComboLabel _comboLabelPrefab;

        private int _comboCounter;
        private float _lastScoredTime;
        private PlayerScore _playerScore;

        private CancellationTokenSource cancelToken = new();

        public ComboService(ScoreConfig scoreConfig, IScoreService scoreService, ILabelFactory labelFactory, ComboLabel comboLabelPrefab)
        {
            _scoreConfig = scoreConfig;
            _scoreService = scoreService;
            _labelFactory = labelFactory;
            _comboLabelPrefab = comboLabelPrefab;
        }

        public void AddComboScore(Vector2 position)
        {
            CurrentPosition = position;
            CheckCombo();
            _lastScoredTime = Time.time;
        }

        private void CheckCombo()
        {
            cancelToken.Cancel();
            if (_comboCounter == 0 || (_comboCounter < _scoreConfig.MaxComboCount && IsInComboInterval()))
            {
                _comboCounter++;
                cancelToken = new CancellationTokenSource();
                CheckComboAsync();
            } 
            else
            {
                CreateComboInfo();
                _comboCounter = 0;
            }
        }
        
        private async void CheckComboAsync()
        {
            try
            {
                await Task.Delay((int)(_scoreConfig.MaxIntervalForCombo * 1000), cancelToken.Token);

                if (_comboCounter == 1)
                {
                    _comboCounter = 0;
                    return;
                }
                
                CreateComboInfo();
                
                _comboCounter = 0;
                
            }
            catch (TaskCanceledException e)
            {
            }
        }

        private void CreateComboInfo()
        {
            if (_comboCounter <= 1)
            {
                Debug.LogWarning("Incorrect combo counter");
                return;
            }
            
            ComboInfo comboInfo = new ComboInfo()
            {
                ComboCount = _comboCounter,
                ScoreValue =
                    (_scoreConfig.BasicScoreValue * _comboCounter) * _comboCounter -
                    (_scoreConfig.BasicScoreValue * _comboCounter),
                SpawnPosition = CurrentPosition
            };

            _labelFactory.CreateComboLabel(_comboLabelPrefab, comboInfo);
            _scoreService.AddScore(comboInfo.ScoreValue);
        }

        private bool IsInComboInterval()
        {
            return Time.time - _lastScoredTime < _scoreConfig.MaxIntervalForCombo;
        }
    }
}