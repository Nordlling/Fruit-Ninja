using System;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BlockTypesConfig", menuName = "Configs/BlockTypes")]
    public class BlockTypesConfig : ScriptableObject
    {
        public BlockInfo BlockPiece;
        public BlockInfo Block;
        public BlockInfo Bomb;
        public BlockInfo BonusLife;
    }
    
    [Serializable]
    public class BlockInfo
    {
        public BlockPiece BlockPrefab;
        public Splash SplashPrefab;
        public VisualSprites[] VisualSprites;
    }

    [Serializable]
    public class VisualSprites
    {
        public Sprite BlockSprite;
        public Sprite SplashSprite;
    }
}