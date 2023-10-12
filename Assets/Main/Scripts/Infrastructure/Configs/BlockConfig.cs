using System;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BlockConfig", menuName = "Configs/Block")]
    public class BlockConfig : ScriptableObject
    {
        public BlockInfo[] BlockInfos;
    }

    [Serializable]
    public class BlockInfo
    {
        public Sprite BlockSprite;
        public Sprite SplashSprite;
    }
}