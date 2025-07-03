using System.Collections.Generic;
using UnityEngine;

namespace GameRoot
{
    public class Localization : MonoBehaviour
    {
        [SerializeField] private string _defaultLanguage = "ru";

        private string _language;

        private Dictionary<string, string> _texts;

        private readonly Dictionary<string, string> _ru = new()
        {
            { "WindCard", "Торнадо!!!" },
            { "EarthquakeCard", "Дрожь земли!" },
            { "GlitchCard", "Случайные блоки зависают и не поддаются извлечению" },
            { "RotatingPlatformCard", "Башня начинает крутиться" },
            { "MagneticCard", "Блоки притягиваются друг к другу" },
            { "GhostCard", "Блоки становятся невидимыми" },
            { "HeavyCard", "Блоки становятся тяжёлыми" },
            { "SlipperyCard", "Блоки становятся скользкими" },
            { "ExplosiveCard", "Один блок взорвется" },
            { "SmokeCard", "Стало дымно" },
            { "Score", "Очки: " },
            { "The light block", "Светлый блок: " }
        };

        private readonly Dictionary<string, string> _en = new()
        {
            { "WindCard", "Tornado!!!" },
            { "EarthquakeCard", "Earthquake!" },
            { "GlitchCard", "Random blocks freeze and can't be removed" },
            { "RotatingPlatformCard", "The tower starts spinning" },
            { "MagneticCard", "Blocks attract each other" },
            { "GhostCard", "Blocks become invisible" },
            { "HeavyCard", "Blocks become heavy" },
            { "SlipperyCard", "Blocks become slippery" },
            { "ExplosiveCard", "One block will explode" },
            { "SmokeCard", "It's smoky now" },
            { "Score", "Score: " },
            { "The light block", "The light block: " }
        };

        private void Awake()
        {
            string savedLang = PlayerPrefs.GetString("Language", _defaultLanguage);
            SetLanguage(savedLang);
        }

        public void SetLanguage(string lang)
        {
            _language = lang;

            switch (_language)
            {
                case "ru": _texts = _ru; break;
                case "en": _texts = _en; break;
                default: _texts = _en; break;
            }
        }

        public string GetText(string key)
        {
            return _texts != null && _texts.TryGetValue(key, out var value) ? value : key;
        }
    }
}
