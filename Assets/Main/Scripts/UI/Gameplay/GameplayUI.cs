using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class GameplayUI : MonoBehaviour, ILoseable, IGameOverable, IPlayable
    {
        [SerializeField] private UIGameOverView _gameOverView;
        [SerializeField] private UIPauseView _pauseView;
        [SerializeField] private Button _pauseButton;

        private IGameplayStateMachine _gameplayStateMachine;
        private bool _canPause = true;

        public void Construct(IGameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public void GameOver()
        {
            DisplayGameOverUI();
        }

        public void Play()
        {
            _canPause = true;
        }

        public void Lose()
        {
            _canPause = false;
        }

        private void OnEnable()
        {
            
            _pauseButton.onClick.AddListener(PauseGame);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseGame);
        }

        private void PauseGame()
        {
            if (_canPause)
            {
                _gameplayStateMachine.Enter<PauseState>();
                _pauseView.gameObject.SetActive(true);
            }
        }

        private void DisplayGameOverUI()
        {
            _gameOverView.gameObject.SetActive(true);
        }
    }
}