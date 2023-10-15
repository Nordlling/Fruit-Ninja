using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Configs/Score")]
    public class ScoreConfig : ScriptableObject
    {
        public int BasicScoreValue = 50;
    }
}