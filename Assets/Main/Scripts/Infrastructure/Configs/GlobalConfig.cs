using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GlobalConfig", menuName = "Configs/Global")]
    public class GlobalConfig : ScriptableObject
    {
        public bool StartCurrentScene;
        public string InitialScene; 
    }
}