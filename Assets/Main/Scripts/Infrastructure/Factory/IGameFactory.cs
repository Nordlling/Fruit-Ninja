using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    void Cleanup();
  }
}