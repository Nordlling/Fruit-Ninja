using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
  public interface IBlockFactory : IService
  {
    Block CreateBlock(Vector2 position);
    BlockPiece CreateBlockPiece(Vector2 position);
    Splash CreateSplash(Vector2 position);
    void Cleanup();
  }
}