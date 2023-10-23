using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public interface ISliceable
    {
        void Slice(Vector2 swiperPosition, Vector2 swiperDirection);
    }
}