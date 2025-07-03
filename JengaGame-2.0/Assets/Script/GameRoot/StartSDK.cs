using UnityEngine;
using YG;

namespace GameRoot
{
    public class StartSDK : MonoBehaviour
    {
        [SerializeField] private Localization _localization;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += GetLoad;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= GetLoad;
        }

        private void Start()
        {
            if (YandexGame.SDKEnabled == true) 
            {
                GetLoad();
            }

            string lang = YandexGame.EnvironmentData.language.ToLower();

            PlayerPrefs.SetString("Language", lang);
            PlayerPrefs.Save();

            _localization.SetLanguage(lang);
        }

        public void GetLoad()
        {
            YandexGame.GameReadyAPI();
        }
    }
}
