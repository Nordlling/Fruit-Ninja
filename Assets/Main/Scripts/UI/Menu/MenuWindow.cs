using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Infrastructure.States;
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
        
        private IGameStateMachine _stateMachine;
        private IScoreService _scoreService;

        public void Construct(IGameStateMachine stateMachine, IScoreService scoreService)
        {
            _stateMachine = stateMachine;
            _scoreService = scoreService;
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _highScoreText.text = _scoreService.HighScore.ToString();
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
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
        }
    }
}
