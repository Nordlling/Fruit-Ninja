using System;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Combo
{
    public interface IComboService : IService
    {
        void AddComboScore(Vector2 position);
        
        Vector2 CurrentPosition { get; }
    }
}