using Gameplay;
using GameRoot;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIGameplay
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameplayUIElements;
        [SerializeField] private GameObject _pauseMenuCanvas;
        [SerializeField] private GameObject _settingsCanvas;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private ScoreManager _scoreManager;

        private bool _isPaused = false;

        public bool IsPaused => _isPaused;

        private void Start()
        {
            _pauseMenuCanvas.SetActive(false);
            _settingsCanvas.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePauseMenu();

            if (_isPaused)
                return;
        }

        public void TogglePauseMenu()
        {
            _isPaused = !_isPaused;
            _pauseMenuCanvas.SetActive(_isPaused);
            Time.timeScale = _isPaused ? 0 : 1;

            foreach (var uiElement in _gameplayUIElements)
                uiElement.SetActive(!_isPaused);
        }

        public void ContinueGame()
        {
            TogglePauseMenu();
        }

        public void StartNewGame()
        {
            Time.timeScale = 1;

            _scoreManager.ResetScore();

            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == Scenes.GAMEPLAYNEWCHALLENGES)
            {
                SceneManager.LoadScene(Scenes.GAMEPLAYNEWCHALLENGES);
            }
            else if (currentScene.name == Scenes.GAMEPLAYCLASSIC)
            {
                SceneManager.LoadScene(Scenes.GAMEPLAYCLASSIC);
            }
        }

        public void OpenSettings()
        {
            _settingsCanvas.SetActive(true);
            _pauseMenuCanvas.SetActive(false);
        }


        public void CloseSettings()
        {
            _settingsCanvas.SetActive(false);
            _pauseMenuCanvas.SetActive(true);
        }

        public void QuitGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(Scenes.MAIN_MENU);
        }
    }
}
