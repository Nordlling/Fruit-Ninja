namespace Main.Scripts.UI.Gameplay
{
    public class UIHighScoreView : UIScoreView
    {
        private void Start()
        {
            InitScore();
        }

        private void InitScore()
        {
            _scoreValue.text = _scoreService.HighScore.ToString();
        }

        private void OnEnable()
        {
            _scoreService.OnHighScored += AddScore;
        }

        private void OnDisable()
        {
            _scoreService.OnHighScored -= AddScore;
        }
    }
}