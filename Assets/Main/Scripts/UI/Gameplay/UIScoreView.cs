using System.Collections;
using Main.Scripts.Infrastructure.Services.Score;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class UIScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private float _scoreAnimationDuration;
        
        private IScoreService _scoreService;
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
            _scoreText.text = 0.ToString();
        }

        private void OnEnable()
        {
            _scoreService.OnScored += AddScore;
        }

        private void OnDisable()
        {
            _scoreService.OnScored -= AddScore;
        }

        private void AddScore(int newScore)
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