using System;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Magnetism
{
    public interface IMagnetService
    {
        void Attract(Vector2 attractionPosition);
    }
}