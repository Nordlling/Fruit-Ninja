using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "AssetPathConfig", menuName = "Configs/AssetPath")]
    public class AssetPathConfig : ScriptableObject
    {
        public string SpawnerPath;
        public string MenuWindowPath;
        public string BootstrapConfigPath;
    }
}