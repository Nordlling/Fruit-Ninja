using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
  public interface IBlockFactory : IService
  {
    BlockPiece CreateBlockPiece(Vector2 position);
    Block CreateBlock(Vector2 position, float invulnerabilityDuration);
    Bomb CreateBomb(Vector2 position);
    BonusLife CreateBonusLife(Vector2 position);
    BlockBag CreateBlockBag(Vector2 position);
    Freeze CreateFreeze(Vector2 position);
    Magnet CreateMagnet(Vector2 position);
    Brick CreateBrick(Vector2 position);
    void Cleanup();
  }
}