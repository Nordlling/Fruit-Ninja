using Main.Scripts.Infrastructure.Services.Restart;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class UIGameOverView : MonoBehaviour
    {
        [SerializeField] private string _menuSceneName;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        
        [SerializeField] private Animation _animation;
        [SerializeField] private AnimationClip _animationFadeIn;
        [SerializeField] private AnimationClip _animationFadeOut;


        private IGameStateMachine _stateMachine;
        private IRestartService _restartService;
        private IScoreService _scoreService;

        public void Construct(IGameStateMachine stateMachine, IRestartService restartService, IScoreService scoreService)
        {
            _stateMachine = stateMachine;
            _restartService = restartService;
            _scoreService = scoreService;
        }

        private void OnEnable()
        {
            PlayAnimationFadeIn();
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ExitToMenu);
            InitScore();
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ExitToMenu);
        }

        private void InitScore()
        {
            _scoreText.text = _scoreService.CurrentScore.ToString();
            _highScoreText.text = _scoreService.HighScore.ToString();
        }

        private void RestartGame()
        {
            PlayAnimationFadeOut();
            _restartService.Restart();
        }

        private void PlayAnimationFadeIn()
        {
            _animation.clip = _animationFadeIn;
            _animation.Play();
        }

        private void PlayAnimationFadeOut()
        {
            _animationFadeOut.AddEvent(new AnimationEvent
            {
                time = _animationFadeOut.length,
                functionName = nameof(Disable)
            });
            _animation.clip = _animationFadeOut;
            _animation.Play();
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        private void ExitToMenu()
        {
            _stateMachine.Enter<LoadSceneState, string>(_menuSceneName);
        }
    }
}