using System;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Loading;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class UIPauseView : MonoBehaviour
    {
        [SerializeField] private string _menuSceneName;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _menuButton;

        [SerializeField] private UICurtainView _curtainView;


        private IGameStateMachine _stateMachine;
        private IGameplayStateMachine _gameplayStateMachine;
        private bool isTouched;
        private Action OnFinished;

        public void Construct(IGameStateMachine stateMachine, IGameplayStateMachine gameplayStateMachine)
        {
            _stateMachine = stateMachine;
            _gameplayStateMachine = gameplayStateMachine;
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueGame);
            _menuButton.onClick.AddListener(ExitToMenu);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueGame);
            _menuButton.onClick.RemoveListener(ExitToMenu);
        }

        private void ContinueGame()
        {
            _gameplayStateMachine.Enter<PlayState>();
            gameObject.SetActive(false);
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
        
    }
}