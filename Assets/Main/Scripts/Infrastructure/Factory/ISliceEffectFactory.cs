using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
    public interface ISliceEffectFactory
    {
        Splash CreateBlockSplash(Vector2 position, int visualIndex);
        Splash CreateBombSplash(Vector2 position, int visualIndex);
        Splash CreateBonusLifeSplash(Vector2 position, int visualIndex);
        Splash CreateBlockBagSplash(Vector2 position, int visualIndex);
        Splash CreateFreezeSplash(Vector2 position, int visualIndex);
        Splash CreateMagnetSplash(Vector2 position, int visualIndex);
        MagnetAreaEffect CreateMagnetAreaEffect(Vector2 position);
        Splash CreateBrickSplash(Vector2 position, int visualIndex);
        Splash CreateSamuraiSplash(Vector2 position, int visualIndex);
        void Cleanup();
    }
}