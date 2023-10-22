using System;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.ButtonContainer;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Loading;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class UIPauseView : MonoBehaviour
    {
        [SerializeField] private PauseAnimation _pauseAnimation;
        
        [SerializeField] private string _menuSceneName;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _menuButton;

        [SerializeField] private UICurtainView _curtainView;

        private IGameStateMachine _stateMachine;
        private IGameplayStateMachine _gameplayStateMachine;
        private IButtonContainerService _buttonContainerService;
        private Action OnFinished;

        public void Construct(
            IGameStateMachine stateMachine, 
            IGameplayStateMachine gameplayStateMachine, 
            IButtonContainerService buttonContainerService)
        {
            _stateMachine = stateMachine;
            _gameplayStateMachine = gameplayStateMachine;
            _buttonContainerService = buttonContainerService;
            
            AddButtonsToContainer();
            OnFinished += ChangeToPlayState;
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueGame);
            _menuButton.onClick.AddListener(ExitToMenu);

            _pauseAnimation.PlayFadeInAnimation();
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueGame);
            _menuButton.onClick.RemoveListener(ExitToMenu);
        }
        
        private void AddButtonsToContainer()
        {
            _buttonContainerService.AddButton(_continueButton);
            _buttonContainerService.AddButton(_menuButton);
        }

        private void ContinueGame()
        {
            _pauseAnimation.PlayFadeOutAnimation(OnFinished);
        }

        private void ChangeToPlayState()
        {
            _gameplayStateMachine.Enter<PlayState>();
            gameObject.SetActive(false);
        }

        private void ExitToMenu()
        {
            _curtainView.gameObject.SetActive(true);
            _curtainView.FadeInBackground(() => _stateMachine.Enter<LoadSceneState, string>(_menuSceneName));
        }
        
    }
}