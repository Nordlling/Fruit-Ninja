using Main.Scripts.Data;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress(PlayerProgress playerProgress);
    PlayerProgress LoadProgress();
  }
}