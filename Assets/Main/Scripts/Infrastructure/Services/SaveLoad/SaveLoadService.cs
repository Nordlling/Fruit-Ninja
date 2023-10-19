using System.Threading;
using System.Threading.Tasks;
using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string _progressKey = "PlayerScore";
    private const int _delay = 2000;
   
    private CancellationTokenSource cancelToken = new();

    public void SaveProgress(PlayerScore playerScore)
    {
      cancelToken.Cancel();
      cancelToken = new CancellationTokenSource();
      SaveAsync(playerScore);
    }

    public PlayerScore LoadProgress()
    {
      return JsonUtility.FromJson<PlayerScore>(PlayerPrefs.GetString(_progressKey));
    }

    private async void SaveAsync(PlayerScore playerScore)
    {
      try
      {
        await Task.Delay(_delay, cancelToken.Token);

        PlayerPrefs.SetString(_progressKey, JsonUtility.ToJson(playerScore));
        PlayerPrefs.Save();
      }
      catch (TaskCanceledException e)
      {
      }
    }
    
  }
}