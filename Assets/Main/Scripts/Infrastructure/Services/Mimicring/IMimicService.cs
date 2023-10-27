using System;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Mimicring
{
    public interface IMimicService : IService
    {
        BlockPiece GetRandomMimicBlock(Type blockType, Vector2 position);
    }
}