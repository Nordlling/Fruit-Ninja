using UnityEngine;

namespace Main.Scripts.Logic.Trail
{
    public interface ISwiper
    {
        float Speed { get; }

        Vector2 Position { get; }

        bool HasEnoughSpeed();
    }
}