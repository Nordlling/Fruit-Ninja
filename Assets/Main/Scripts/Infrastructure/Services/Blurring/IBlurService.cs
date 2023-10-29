using System;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Blurring
{
    public interface IBlurService : IService
    {
        bool Blurred { get; }
        bool Enabled { get; }
        event Action OnBlurred;
        event Action OnUnblurred;
        bool TryGetBlurredSprite(out Sprite sprite, BlockPiece block, int spriteIndex);
        void BlurAll();
        void UnblurAll();
        Sprite BlurSprite(Texture2D texture);
    }
}