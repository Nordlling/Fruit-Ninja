using System;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Combo
{
    public interface IComboService : IService
    {
        event Action<ComboInfo> OnComboScored;
        
        void AddComboScore(Vector2 position);
        
        Vector2 CurrentPosition { get; }
    }
}