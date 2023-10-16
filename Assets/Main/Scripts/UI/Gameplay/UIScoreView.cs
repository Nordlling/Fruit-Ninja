using System.Collections;
using Main.Scripts.Infrastructure.Services.Score;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class UIScoreView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _scoreText;
        [SerializeField] protected float _scoreAnimationDuration;
        
        protected IScoreService _scoreService;
        private int _currentScore;
        private Coroutine _currentCoroutine;

        public void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void Start()
        {
            InitScore();
        }

        private void InitScore()
        {
            _currentScore = 0;
            _scoreText.text = _currentScore.ToString();
        }

        private void OnEnable()
        {
            _scoreService.OnScored += AddScore;
            _scoreService.OnReset += InitScore;
        }

        private void OnDisable()
        {
            _scoreService.OnScored -= AddScore;
            _scoreService.OnReset -= InitScore;
        }

        protected void AddScore(int newScore)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _scoreText.text = _currentScore.ToString();
            _currentCoroutine = StartCoroutine(StartScoreAnimation(_currentScore, newScore));
            _currentScore = newScore;
        }
        
        private IEnumerator StartScoreAnimation(int oldScore, int newScore)
        {
            float speed = _scoreAnimationDuration / (newScore - oldScore);
            for (int i = oldScore; i < newScore + 1; i++)
            {
                _scoreText.text = i.ToString();
                yield return new WaitForSeconds(speed);
            }
        }
    }
}