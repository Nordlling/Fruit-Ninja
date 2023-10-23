using Main.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.AnimationTargetContainer
{
    public interface IAnimationTargetContainer : IService
    {
        Vector2 HealthTarget { get; set; }

    }
}