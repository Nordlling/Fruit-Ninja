using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Score;
using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Block CreateBlock(Block blockPrefab, Vector2 position);
    BlockPiece CreateBlockPiece(BlockPiece blockPrefab, Vector2 position);
    Splash CreateSplash(Splash splashPrefab, Vector2 position);
    ScoreLabel CreateScoreLabel(ScoreLabel scoreLabelPrefab, Vector2 position, string value);
    void Cleanup();
  }
}