using Main.Scripts.Infrastructure.AssetManagment;
using Main.Scripts.Infrastructure.Configs;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Config
{
  public class ConfigService : IConfigService
  {
    private GlobalConfig _globalConfig;

    public void Load()
    {
      _globalConfig = Resources.Load<GlobalConfig>(AssetPath.GlobalConfigPath);
    }
    
    public GlobalConfig ForGlobalConfig()
    {
      return _globalConfig;
    }
  }
}