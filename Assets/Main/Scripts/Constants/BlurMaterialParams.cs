using UnityEngine;

namespace Main.Scripts.Constants
{
    public class BlurMaterialParams
    {
        public static readonly int BlurTex = Shader.PropertyToID("_BlurTex");
        public static readonly int Radius = Shader.PropertyToID("_Radius");
        public static readonly int Step = Shader.PropertyToID("_Step");
        public static readonly int Jump = Shader.PropertyToID("_Jump");
    }
}