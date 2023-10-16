using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    
    public void SaveProgress(PlayerProgress playerProgress)
    {
      PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(playerProgress));
      PlayerPrefs.Save();
    }

    public PlayerProgress LoadProgress()
    {
      return JsonUtility.FromJson<PlayerProgress>(PlayerPrefs.GetString(ProgressKey));
    }
  }
}