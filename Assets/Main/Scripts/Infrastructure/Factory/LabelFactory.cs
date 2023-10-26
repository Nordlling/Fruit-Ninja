using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.AnimationTargetContainer;
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
        private readonly ServiceContainer _serviceContainer;
        private readonly WordEndingsConfig _wordEndingsConfig;

        public LabelFactory(ServiceContainer serviceContainer,  WordEndingsConfig wordEndingsConfig)
        {
            _serviceContainer = serviceContainer;
            _wordEndingsConfig = wordEndingsConfig;
        }

        public ScoreLabel CreateScoreLabel(ScoreLabel scoreLabelPrefab, Vector2 position, string value)
        {
            ScoreLabel scoreLabel = Object.Instantiate(scoreLabelPrefab, position, Quaternion.identity);
            scoreLabel.Construct(value, _serviceContainer.Get<ITimeProvider>());
            return scoreLabel;
        }
        
        public ComboLabel CreateComboLabel(ComboLabel comboLabelPrefab, ComboInfo comboInfo)
        {
            ComboLabel comboLabel = Object.Instantiate(comboLabelPrefab, comboInfo.SpawnPosition, Quaternion.identity);
            Dictionary<int, string> fruitDictionary = _wordEndingsConfig.FruitDictionary.ToDictionary(key => key.Number, value => value.Word);
            comboLabel.Construct(comboInfo.ComboCount, _serviceContainer.Get<ITimeProvider>(), _serviceContainer.Get<LivingZone>(), fruitDictionary);
            return comboLabel;
        }

        public ExplosionLabel CreateExplosionLabel(ExplosionLabel explosionLabelPrefab, Vector2 position)
        {
            ExplosionLabel explosionLabel = Object.Instantiate(explosionLabelPrefab, position, Quaternion.identity);
            explosionLabel.Construct(_serviceContainer.Get<ITimeProvider>());
            return explosionLabel;
        }
        
        public HealthLabel CreateHealthLabel(HealthLabel healthLabelPrefab, Vector2 position)
        {
            HealthLabel healthLabel = Object.Instantiate(healthLabelPrefab, position, Quaternion.identity);
            healthLabel.Construct(_serviceContainer.Get<ITimeProvider>(), _serviceContainer.Get<IAnimationTargetContainer>().HealthTarget);
            return healthLabel;
        }

        public void Cleanup()
        {
        }
    }
}