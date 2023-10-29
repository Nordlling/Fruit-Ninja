using System;
using System.Collections.Generic;
using Main.Scripts.Constants;
using Main.Scripts.Infrastructure.Configs;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBags;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using Main.Scripts.Logic.Blocks.Mimics;
using Main.Scripts.Logic.Blocks.Samurais;
using Main.Scripts.Utils.TextureUtils;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Blurring
{
    public class BlurService : IBlurService
    {
        private readonly BlurConfig _blurConfig;
        private Material _blurMaterial;

        private Dictionary<Type, VisualSprites[]> _originalSprites;
        private Dictionary<Type, List<Sprite>> _blurredSprites;

        public bool Blurred { get; private set; }
        public bool Enabled => _blurConfig.Enabled;
        public event Action OnBlurred;
        public event Action OnUnblurred;

        public BlurService(BlurConfig blurConfig)
        {
            _blurConfig = blurConfig;
        }

        public void Init()
        {
            _blurMaterial = new Material(_blurConfig.BlurMaterial);

            _originalSprites = new()
            {
                { typeof(BlockPiece), _blurConfig.BlocksConfig.BlockPiece.VisualSprites },
                { typeof(Block), _blurConfig.BlocksConfig.Block.VisualSprites },
                { typeof(Bomb), _blurConfig.BlocksConfig.BombConfig.BlockInfo.VisualSprites },
                { typeof(BonusLife), _blurConfig.BlocksConfig.BonusLifeConfig.BlockInfo.VisualSprites },
                { typeof(BlockBag), _blurConfig.BlocksConfig.BlockBagConfig.BlockInfo.VisualSprites },
                { typeof(Freeze), _blurConfig.BlocksConfig.FreezeConfig.BlockInfo.VisualSprites },
                { typeof(Magnet), _blurConfig.BlocksConfig.MagnetConfig.BlockInfo.VisualSprites },
                { typeof(Brick), _blurConfig.BlocksConfig.BrickConfig.BlockInfo.VisualSprites },
                { typeof(Samurai), _blurConfig.BlocksConfig.SamuraiConfig.BlockInfo.VisualSprites },
                { typeof(Mimic), _blurConfig.BlocksConfig.MimicConfig.BlockInfo.VisualSprites }
            };

            _blurredSprites = new()
            {
                { typeof(BlockPiece), new List<Sprite>() },
                { typeof(Block), new List<Sprite>() },
                { typeof(Bomb), new List<Sprite>() },
                { typeof(BonusLife), new List<Sprite>() },
                { typeof(BlockBag), new List<Sprite>() },
                { typeof(Freeze), new List<Sprite>() },
                { typeof(Magnet), new List<Sprite>() },
                { typeof(Brick), new List<Sprite>() },
                { typeof(Samurai), new List<Sprite>() },
                { typeof(Mimic), new List<Sprite>() }
            };

            CreateBlurredImages();
        }

        public bool TryGetBlurredSprite(out Sprite sprite, BlockPiece block, int spriteIndex)
        {
            if (!_blurConfig.Enabled)
            {
                sprite = null;
                return false;
            }
            
            sprite = _blurredSprites[block.GetType()][spriteIndex];
            return Blurred;
        }

        public void BlurAll()
        {
            if (!_blurConfig.Enabled)
            {
                return;
            }
            
            Blurred = true;
            OnBlurred?.Invoke();
        }
        
        public void UnblurAll()
        {
            if (!_blurConfig.Enabled)
            {
                return;
            }
            
            Blurred = false;
            OnUnblurred?.Invoke();
        }

        public Sprite BlurSprite(Texture2D texture)
        {
            _blurMaterial.SetTexture(BlurMaterialParams.BlurTex, texture);
            return _blurMaterial.CreateRenderSprite(texture.width, texture.height);
        }

        private void CreateBlurredImages()
        {
            foreach (KeyValuePair<Type, VisualSprites[]> kvp in _originalSprites)
            {
                foreach (VisualSprites visualSprite in kvp.Value)
                {
                    Sprite sprite = BlurSprite(visualSprite.BlockSprite.texture);
                    _blurredSprites[kvp.Key].Add(sprite);
                }
            }
        }
        
    }
}