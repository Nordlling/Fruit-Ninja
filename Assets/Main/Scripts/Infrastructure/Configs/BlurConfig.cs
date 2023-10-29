using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BlurConfig", menuName = "Configs/Blur")]
    public class BlurConfig : ScriptableObject
    {
        public BlocksConfig BlocksConfig;
        public Material BlurMaterial;
        [FormerlySerializedAs("Enable")] public bool Enabled = true;
    }
}