using Gameplay;
using GameRoot;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIGameplay
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverUIElement;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Canvas[] _canvasesToDisable;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private GameEvents _gameEvents;

        private void OnEnable()
        {
            _gameEvents.GameOver += ShowGameOverUI;
        }

        private void OnDisable()
        {
            _gameEvents.GameOver -= ShowGameOverUI;

        } 

        private void Start()
        {
            _gameOverUIElement.SetActive(false);
            _mainMenuButton.onClick.AddListener(LoadMainMenu);
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void ShowGameOverUI()
        {
            _gameOverUIElement.SetActive(true);
            Time.timeScale = 0f;
            BlockOtherUIInteraction();
        }

        private void BlockOtherUIInteraction()
        {
            foreach (var canvas in _canvasesToDisable)
                if (canvas != null && canvas.gameObject != _gameOverUIElement)
                    canvas.enabled = false;
        }

        private void LoadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(Scenes.MAIN_MENU);
        }

        private void RestartGame()
        {
            Time.timeScale = 1;

            _scoreManager.ResetScore();

            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == Scenes.GAMEPLAYNEWCHALLENGES)
                SceneManager.LoadScene(Scenes.GAMEPLAYNEWCHALLENGES);
            else if (currentScene.name == Scenes.GAMEPLAYCLASSIC)
                SceneManager.LoadScene(Scenes.GAMEPLAYCLASSIC);
        }
    }
}
