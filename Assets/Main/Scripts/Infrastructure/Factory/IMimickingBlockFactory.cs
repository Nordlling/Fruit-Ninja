using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBags;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Samurais;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
    public interface IMimickingBlockFactory
    {
        Block CreateMimickingBlock(Vector2 position);
        Bomb CreateMimickingBomb(Vector2 position);
        BonusLife CreateMimickingBonusLife(Vector2 position);
        BlockBag CreateMimickingBlockBag(Vector2 position);
        Freeze CreateMimickingFreeze(Vector2 position);
        Magnet CreateMimickingMagnet(Vector2 position);
        Brick CreateMimickingBrick(Vector2 position);
        Samurai CreateMimickingSamurai(Vector2 position);
    }
}