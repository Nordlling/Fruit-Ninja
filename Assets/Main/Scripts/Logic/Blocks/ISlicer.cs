using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public interface ISlicer
    {
        void Slice(Vector2 swiperPosition, Vector2 swiperDirection);
    }
}