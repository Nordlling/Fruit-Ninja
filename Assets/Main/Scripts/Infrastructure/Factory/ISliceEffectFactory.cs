using Main.Scripts.Logic.Splashing;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Factory
{
    public interface ISliceEffectFactory
    {
        Splash CreateBlockSplash(Vector2 position);
        Splash CreateBombSplash(Vector2 position);
        Splash CreateBonusLifeSplash(Vector2 position);
        void Cleanup();
    }
}