using System;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BlockTypesConfig", menuName = "Configs/BlockTypes")]
    public class BlockTypesConfig : ScriptableObject
    {
        public BlockInfo BlockPiece;
        public BlockInfo Block;
        public BlockInfo Bomb;
    }
    
    [Serializable]
    public class BlockInfo
    {
        public BlockPiece BlockPrefab;
        public VisualSprites[] VisualSprites;
    }

    [Serializable]
    public class VisualSprites
    {
        public Sprite BlockSprite;
        public Sprite SplashSprite;
    }
}