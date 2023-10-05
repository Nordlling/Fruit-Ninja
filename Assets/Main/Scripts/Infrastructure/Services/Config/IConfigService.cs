using Main.Scripts.Infrastructure.Configs;

namespace Main.Scripts.Infrastructure.Services.Config
{
  public interface IConfigService : IService
  {
    void Load();
    GlobalConfig ForGlobalConfig();
  }
}