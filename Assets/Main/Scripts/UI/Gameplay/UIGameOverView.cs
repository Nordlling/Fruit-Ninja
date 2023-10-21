using System;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Loading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class UIGameOverView : MonoBehaviour
    {
        [SerializeField] private GameOverAnimation _gameOverAnimation;
        [SerializeField] private string _menuSceneName;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TextMeshProUGUI _scoreValue;
        [SerializeField] private TextMeshProUGUI _highScoreValue;
        
        [SerializeField] private UICurtainView _curtainView;
        
        private IGameStateMachine _stateMachine;
        private IGameplayStateMachine _gameplayStateMachine;
        private IScoreService _scoreService;
        private bool isTouched;

        private Action OnFinished;

        public void Construct(
            IGameStateMachine stateMachine, 
            IGameplayStateMachine gameplayStateMachine, 
            IScoreService scoreService)
        {
            _stateMachine = stateMachine;
            _gameplayStateMachine = gameplayStateMachine;
            _scoreService = scoreService;

            OnFinished += ChangeToPlayState;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ExitToMenu);
           
            _gameOverAnimation.PlayFadeInAnimation();
            InitScore();
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ExitToMenu);
        }

        private void InitScore()
        {
            _scoreValue.text = _scoreService.CurrentScore.ToString();
            _highScoreValue.text = _scoreService.HighScore.ToString();
        }

        private void RestartGame()
        {
            if (isTouched)
            {
                return;
            }

            isTouched = true;
            
            _gameOverAnimation.PlayFadeOutAnimation(OnFinished);
            _gameplayStateMachine.Enter<RestartState>();
        }

        private void ExitToMenu()
        {
            if (isTouched)
            {
                return;
            }
            
            isTouched = true;
            
            _curtainView.gameObject.SetActive(true);
            _curtainView.FadeInBackground(() => _stateMachine.Enter<LoadSceneState, string>(_menuSceneName));
        }

        private void ChangeToPlayState()
        {
            _gameplayStateMachine.Enter<PlayState>();
            gameObject.SetActive(false);
        }
    }
}