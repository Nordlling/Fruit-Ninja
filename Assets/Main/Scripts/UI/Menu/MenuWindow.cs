using Main.Scripts.Infrastructure.Services.ButtonContainer;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Loading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Menu
{
    public class MenuWindow : MonoBehaviour
    {
        [SerializeField] private string _transferSceneName;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        [SerializeField] private UICurtainView _curtainView;
        
        private IGameStateMachine _stateMachine;
        private IScoreService _scoreService;
        private IButtonContainerService _buttonContainerService;

        public void Construct(IGameStateMachine stateMachine, IScoreService scoreService, IButtonContainerService buttonContainerService)
        {
            _stateMachine = stateMachine;
            _scoreService = scoreService;
            _buttonContainerService = buttonContainerService;
            AddButtonsToContainer();
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _highScoreText.text = _scoreService.HighScore.ToString();
        }

        private void En()
        {
            _startButton.interactable = false;
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _exitButton.onClick.RemoveListener(ExitGame);
        }

        private void AddButtonsToContainer()
        {
            _buttonContainerService.AddButton(_startButton);
            _buttonContainerService.AddButton(_exitButton);
        }

        private void StartGame()
        {
            _curtainView.gameObject.SetActive(true);
            _buttonContainerService.DisableAllButtons();
            _curtainView.FadeInBackground(() => _stateMachine.Enter<LoadSceneState, string>(_transferSceneName));
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
        }
    }
}
