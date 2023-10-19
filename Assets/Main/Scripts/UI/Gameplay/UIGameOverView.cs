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
        [SerializeField] private string _menuSceneName;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        
        [SerializeField] private Animation _animation;
        [SerializeField] private AnimationClip _animationFadeIn;
        [SerializeField] private AnimationClip _animationFadeOut;
        
        [SerializeField] private UICurtainView _curtainView;


        private IGameStateMachine _stateMachine;
        private IGameplayStateMachine _gameplayStateMachine;
        private IScoreService _scoreService;
        private bool isTouched;

        public void Construct(IGameStateMachine stateMachine, IGameplayStateMachine gameplayStateMachine, IScoreService scoreService)
        {
            _stateMachine = stateMachine;
            _gameplayStateMachine = gameplayStateMachine;
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
            if (isTouched)
            {
                return;
            }

            isTouched = true;
            
            PlayAnimationFadeOut();
            _gameplayStateMachine.Enter<RestartState>();
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
            isTouched = false;
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