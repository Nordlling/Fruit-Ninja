using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
    public interface ISliceEffectFactory
    {
        Splash CreateBlockSplash(Vector2 position, int visualIndex);
        Splash CreateBombSplash(Vector2 position, int visualIndex);
        Splash CreateBonusLifeSplash(Vector2 position, int visualIndex);
        void Cleanup();
    }
}