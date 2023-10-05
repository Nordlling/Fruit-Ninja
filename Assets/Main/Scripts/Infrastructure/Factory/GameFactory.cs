using Main.Scripts.Infrastructure.AssetManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public void CreateSpawner()
    {
      var prefab = Resources.Load<GameObject>(AssetPath.SpawnerPath);
      Object.Instantiate(prefab);
    }

    public void Cleanup()
    {
      
    }
  }
}