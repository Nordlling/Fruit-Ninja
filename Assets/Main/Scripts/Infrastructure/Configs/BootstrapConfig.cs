using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BootstrapConfig", menuName = "Configs/Bootstrap")]
    public class BootstrapConfig : ScriptableObject
    {
        public bool StartCurrentScene;
        public string InitialScene; 
    }
}