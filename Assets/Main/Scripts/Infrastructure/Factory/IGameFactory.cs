using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Block CreateBlock(Block blockPrefab, Vector2 position);
    BlockPiece CreateBlockPiece(BlockPiece blockPrefab, Vector2 position);
    void Cleanup();
  }
}