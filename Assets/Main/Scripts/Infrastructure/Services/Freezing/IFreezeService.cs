using System;

namespace Main.Scripts.Infrastructure.Services.Freezing
{
    public interface IFreezeService
    {
        event Action OnFreezed;
        event Action OnUnfreezed;
        void Freeze();
    }
}