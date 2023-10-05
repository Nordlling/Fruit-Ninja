using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class MenuWindow : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

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
            Debug.Log("Start");
        }

        private void ExitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
