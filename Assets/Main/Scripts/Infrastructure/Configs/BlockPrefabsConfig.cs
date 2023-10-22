using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "BlockPrefabsConfig", menuName = "Configs/BlockPrefabs")]
    public class BlockPrefabsConfig : ScriptableObject
    {
        public BlockPiece BlockPiecePrefab;
        public Block BlockPrefab;
    }
}