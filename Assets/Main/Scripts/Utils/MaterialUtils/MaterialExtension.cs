using UnityEngine;

namespace Main.Scripts.Utils.MaterialUtils
{
    public static class MaterialExtension
    {
        public static Sprite CreateRenderSprite(this Material material, int textureWidth, int textureHeight)
        {
            return material.CreateRenderSprite(textureWidth, textureHeight, new Rect(0, 0, textureWidth, textureHeight), new Vector2(0.5f, 0.5f));
        }
        
        public static Sprite CreateRenderSprite(this Material material, int textureWidth, int textureHeight, Rect rect, Vector2 pivot)
        {
            RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 0, RenderTextureFormat.ARGB32);
            Graphics.Blit(null, renderTexture, material);
            Texture2D blurredTexture = new Texture2D(textureWidth, textureHeight);
            RenderTexture.active = renderTexture;
            blurredTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            blurredTexture.Apply();
            RenderTexture.active = null;

            return Sprite.Create(blurredTexture, rect, pivot);
        }
    }
}