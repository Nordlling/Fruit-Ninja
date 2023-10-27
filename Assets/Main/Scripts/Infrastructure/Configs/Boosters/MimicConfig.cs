using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBags;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Samurais;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs.Boosters
{
    [CreateAssetMenu(fileName = "MimicConfig", menuName = "Configs/Boosters/Mimic")]
    public class MimicConfig : BoosterConfig
    {
        [Header("Booster Settings")]
        public float TransformationTime;
        public float PrepareTime;
        
        [Header("Mimicking blocks prefabs")]
        public Block BlockPrefab;
        public Bomb BombPrefab;
        public BonusLife BonusLifePrefab;
        public BlockBag BlockBagPrefab;
        public Freeze FreezePrefab;
        public Magnet MagnetPrefab;
        public Brick BrickPrefab;
        public Samurai SamuraiPrefab;
    }
}