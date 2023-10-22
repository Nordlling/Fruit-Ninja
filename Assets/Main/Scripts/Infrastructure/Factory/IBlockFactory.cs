using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
  public interface IBlockFactory : IService
  {
    BlockPiece CreateBlockPiece(Vector2 position);
    Block CreateBlock(Vector2 position);
    Bomb CreateBomb(Vector2 position);
    void Cleanup();
  }
}