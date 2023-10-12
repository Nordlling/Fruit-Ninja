using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class MenuWindow : MonoBehaviour
    {
        [SerializeField] private string _transferSceneName;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        private IGameStateMachine _stateMachine;

        public void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _exitButton.onClick.RemoveListener(ExitGame);
        }

        private void StartGame()
        {
            _stateMachine.Enter<LoadSceneState, string>(_transferSceneName);
        }

        private void ExitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
