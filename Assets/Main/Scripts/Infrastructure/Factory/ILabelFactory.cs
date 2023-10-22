using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Logic.Combo;
using Main.Scripts.Logic.Label;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
    public interface ILabelFactory
    {
        ScoreLabel CreateScoreLabel(ScoreLabel scoreLabelPrefab, Vector2 position, string value);
        ComboLabel CreateComboLabel(ComboLabel comboLabelPrefab, ComboInfo comboInfo);
        ExplosionLabel CreateExplosionLabel(ExplosionLabel explosionLabelPrefab, Vector2 position);
        void Cleanup();
    }
}