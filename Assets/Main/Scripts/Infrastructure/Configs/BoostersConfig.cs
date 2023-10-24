using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BoostersConfig", menuName = "Configs/Boosters")]
    public class BoostersConfig : ScriptableObject
    {
        public BoostersSpawnConfig BoostersSpawnConfig;

        public BlockBagConfig BlockBagConfig;
    }
}