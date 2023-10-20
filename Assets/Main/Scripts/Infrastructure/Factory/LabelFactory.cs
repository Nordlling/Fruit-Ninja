using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.LivingZone;
using Main.Scripts.Logic.Combo;
using Main.Scripts.Logic.Score;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Infrastructure.Factory
{
    public class LabelFactory : ILabelFactory
    {
        private readonly ITimeProvider _timeProvider;
        private readonly LivingZone _livingZone;

        public LabelFactory(ITimeProvider timeProvider, LivingZone livingZone)
        {
            _timeProvider = timeProvider;
            _livingZone = livingZone;
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
            comboLabel.Construct(comboInfo.ComboCount, _timeProvider, _livingZone);
            return comboLabel;
        }

        public void Cleanup()
        {
        }
    }
}