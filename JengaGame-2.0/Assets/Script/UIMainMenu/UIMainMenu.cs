using GameRoot;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace UIMainMenu
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsCanvas;
        [SerializeField] private GameObject _leaderboardCanvas;
        [SerializeField] private GameObject _shopCanvas;
        [SerializeField] private GameObject _authorizationPanel;

        [SerializeField] private Button _newGameChallengesButton;
        [SerializeField] private Button _newGameClassicButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _closeSettingsButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _closeLeaderboardButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _closeShopButton;
        [SerializeField] private Button _authorizatioButton;
        [SerializeField] private Button _authorizatioCancelButton;

        private void Start()
        {
            SetCanvasActive(_settingsCanvas, false);
            SetCanvasActive(_leaderboardCanvas, false);
            SetCanvasActive(_shopCanvas, false);
            SetCanvasActive(_authorizationPanel, false);

            _settingsButton.onClick.AddListener(OnSettingsOpen);
            _closeSettingsButton.onClick.AddListener(OnSettingsClose);
            _shopButton.onClick.AddListener(OnShopOpen);
            _closeShopButton.onClick.AddListener(OnShopClose);
            _leaderboardButton.onClick.AddListener(OnLeaderboardClick);
            _closeLeaderboardButton.onClick.AddListener(OnLeaderboardClose);
            _newGameChallengesButton.onClick.AddListener(StartNewGameChallenges);
            _newGameClassicButton.onClick.AddListener(StartNewGameClassic);
            _authorizatioButton.onClick.AddListener(OnAuthPromptAuth);
            _authorizatioCancelButton.onClick.AddListener(OnAuthCancel);

            YandexGame.GetDataEvent += OnYandexLoggedIn;
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(OnSettingsOpen);
            _closeSettingsButton.onClick.RemoveListener(OnSettingsClose);
            _shopButton.onClick.RemoveListener(OnShopOpen);
            _closeShopButton.onClick.RemoveListener(OnShopClose);
            _leaderboardButton.onClick.RemoveListener(OnLeaderboardClick);
            _closeLeaderboardButton.onClick.RemoveListener(OnLeaderboardClose);
            _newGameChallengesButton.onClick.RemoveListener(StartNewGameChallenges);
            _newGameClassicButton.onClick.RemoveListener(StartNewGameClassic);
            _authorizatioButton.onClick.RemoveListener(OnAuthPromptAuth);
            _authorizatioCancelButton.onClick.RemoveListener(OnAuthCancel);

            YandexGame.GetDataEvent -= OnYandexLoggedIn;
        }

        public void StartNewGameChallenges()
        {
            SceneManager.LoadScene(Scenes.GAMEPLAYNEWCHALLENGES);
        }

        public void StartNewGameClassic()
        {
            SceneManager.LoadScene(Scenes.GAMEPLAYCLASSIC);
        }

        private void OnYandexLoggedIn()
        {
            if (!YandexGame.auth) return;

            if (_authorizationPanel.activeSelf)
                SetCanvasActive(_authorizationPanel, false);

            SetCanvasActive(_leaderboardCanvas, true);
        }

        private void SetCanvasActive(GameObject canvas, bool isActive)
        {
            canvas.SetActive(isActive);
        }

        private void OnSettingsOpen() => SetCanvasActive(_settingsCanvas, true);
        private void OnSettingsClose() => SetCanvasActive(_settingsCanvas, false);

        private void OnShopOpen() => SetCanvasActive(_shopCanvas, true);
        private void OnShopClose() => SetCanvasActive(_shopCanvas, false);

        private void OnLeaderboardClick()
        {
            if (YandexGame.auth)
                SetCanvasActive(_leaderboardCanvas, true);
            else
                SetCanvasActive(_authorizationPanel, true);
        }

        private void OnLeaderboardClose() => SetCanvasActive(_leaderboardCanvas, false);

        private void OnAuthPromptAuth() => YandexGame.AuthDialog();

        private void OnAuthCancel() => SetCanvasActive(_authorizationPanel, false);
    }
}
