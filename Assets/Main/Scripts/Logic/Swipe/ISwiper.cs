using UnityEngine;

namespace Main.Scripts.Logic.Swipe
{
    public interface ISwiper
    {
        float Speed { get; }
        
        Vector2 Direction { get; }

        Vector2 Position { get; }

        bool HasEnoughSpeed();
        
        void Block();
    }
}