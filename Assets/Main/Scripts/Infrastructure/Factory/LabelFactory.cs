using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Logic.Combo;
using Main.Scripts.Logic.Label;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Infrastructure.Factory
{
    public class LabelFactory : ILabelFactory
    {
        private readonly ITimeProvider _timeProvider;
        private readonly LivingZone _livingZone;
        private readonly WordEndingsConfig _wordEndingsConfig;

        public LabelFactory(ITimeProvider timeProvider, LivingZone livingZone, WordEndingsConfig wordEndingsConfig)
        {
            _timeProvider = timeProvider;
            _livingZone = livingZone;
            _wordEndingsConfig = wordEndingsConfig;
        }

        public ScoreLabel CreateScoreLabel(ScoreLabel scoreLabelPrefab, Vector2 position, string value)
        {
            ScoreLabel scoreLabel = Object.Instantiate(scoreLabelPrefab, position, Quaternion.identity);
            scoreLabel.Construct(value, _timeProvider);
            return scoreLabel;
        }
        
        public ComboLabel CreateComboLabel(ComboLabel comboLabelPrefab, ComboInfo comboInfo)
        {
            ComboLabel comboLabel = Object.Instantiate(comboLabelPrefab, comboInfo.SpawnPosition, Quaternion.identity);
            Dictionary<int, string> fruitDictionary = _wordEndingsConfig.FruitDictionary.ToDictionary(key => key.Number, value => value.Word);
            comboLabel.Construct(comboInfo.ComboCount, _timeProvider, _livingZone, fruitDictionary);
            return comboLabel;
        }

        public ExplosionLabel CreateExplosionLabel(ExplosionLabel explosionLabelPrefab, Vector2 position)
        {
            ExplosionLabel explosionLabel = Object.Instantiate(explosionLabelPrefab, position, Quaternion.identity);
            explosionLabel.Construct(_timeProvider);
            return explosionLabel;
        }

        public void Cleanup()
        {
        }
    }
}