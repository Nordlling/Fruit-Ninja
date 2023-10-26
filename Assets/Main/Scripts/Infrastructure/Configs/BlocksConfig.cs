using System;
using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BlocksConfig", menuName = "Configs/Blocks")]
    public class BlocksConfig : ScriptableObject
    {
        public BlockInfo BlockPiece;
        public BlockInfo Block;
        
        [Header("Boosters")]
        
        [Range(0, 1)]
        public float MaxFractionInPack;
       
        public BombConfig BombConfig;
        public BonusLifeConfig BonusLifeConfig;
        public BlockBagConfig BlockBagConfig;
        public FreezeConfig FreezeConfig;
        public MagnetConfig MagnetConfig;
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
    
    [Serializable]
    public class BoosterSpawnInfo
    {
        public float DropoutRate;
        [Range(0, 1)]
        public float MaxFractionInBoostPack;
        [Range(-1, 20)]
        public int MaxNumberInBoostPack;
        [Range(-1, 20)]
        public int MaxNumberOnScreen;
    }
}