using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/Health")]
    public class HealthConfig : ScriptableObject
    {
        [Range(1, 8)]
        public int HealthCount = 3;
    }
}