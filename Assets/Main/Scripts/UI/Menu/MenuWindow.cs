using DG.Tweening;
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
        [SerializeField] private Image _backgroundBlur;
        [SerializeField] private Image _leftHouseImage;
        [SerializeField] private Image _rightHouseImage;
        [SerializeField] private Image _lightImage;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _canvasGroupAnimationDuration;
        [SerializeField] private float _blurAnimationDuration;
        [SerializeField] private float _housesAnimationDuration;
        [SerializeField] private float _housesScaleTarget;

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
            StartGameAnimation();
        }

        private void StartGameAnimation()
        {
            Vector2 housesRectWidth = new Vector2(_leftHouseImage.rectTransform.rect.width, 0f) * _housesScaleTarget;
            Vector2 lightRectHeight = new Vector2(0f, _lightImage.rectTransform.rect.height) * _housesScaleTarget;
            
            _leftHouseImage.rectTransform.DOAnchorPos(-housesRectWidth ,_housesAnimationDuration);
            _leftHouseImage.rectTransform.DOScale(_housesScaleTarget ,_housesAnimationDuration);
            
            _rightHouseImage.rectTransform.DOAnchorPos(housesRectWidth, _housesAnimationDuration);
            _rightHouseImage.rectTransform.DOScale(_housesScaleTarget ,_housesAnimationDuration);
            
            _lightImage.rectTransform.DOAnchorPos(lightRectHeight, _housesAnimationDuration);
            _lightImage.rectTransform.DOScale(_housesScaleTarget ,_housesAnimationDuration);

            _canvasGroup.DOFade(0f, _canvasGroupAnimationDuration);
            
            _backgroundBlur.DOFade(0f, _blurAnimationDuration).OnComplete(StartFadeInCurtain);
        }

        private void StartFadeInCurtain()
        {
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
