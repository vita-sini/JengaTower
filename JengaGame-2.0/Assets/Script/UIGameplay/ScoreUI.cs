using Gameplay;
using GameRoot;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace UIGameplay
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private GameEvents _gameEvents;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            _scoreManager.ScoreChanged += UpdateScoreText;
            UpdateScoreText(_scoreManager.CurrentScore);
        }

        private void OnDisable()
        {
            if (_scoreManager != null)
                _scoreManager.ScoreChanged -= UpdateScoreText;
        }

        public void CalculateScore()
        {
            _scoreManager.Add(1);
            _gameEvents.OnInvokeTurnEnd();
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = $"{score}";

            if (score <= 0)
                return;

            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == Scenes.GAMEPLAYNEWCHALLENGES)
            {
                YandexGame.NewLeaderboardScores("NewChallenges", score);
            }
            else if (currentScene.name == Scenes.GAMEPLAYCLASSIC)
            {
                YandexGame.NewLeaderboardScores("Classic", score);
            }
        }
    }
}
