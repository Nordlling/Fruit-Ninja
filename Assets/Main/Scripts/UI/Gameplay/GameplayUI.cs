using Main.Scripts.Infrastructure.Services.GameOver;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private UIGameOverView _gameOverView;
        
        private IGameOverService _gameOverService;

        public void Construct(IGameOverService gameOverService)
        {
            _gameOverService = gameOverService;
        }

        private void OnEnable()
        {
            _gameOverService.OnGameOver += DisplayGameOverUI;
        }

        private void OnDisable()
        {
            _gameOverService.OnGameOver -= DisplayGameOverUI;
        }

        private void DisplayGameOverUI()
        {
            _gameOverView.gameObject.SetActive(true);
        }
    }
}