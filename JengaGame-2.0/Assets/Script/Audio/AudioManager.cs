using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private const string GlobalVolumeKey = "GlobalVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string EffectsVolumeKey = "EffectsVolume";

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;

        [Header("Volume Settings")]
        [Range(0f, 1f)] private float _globalVolume = 1f;
        [Range(0f, 1f)] private float _musicVolume = 1f;
        [Range(0f, 1f)] private float _effectsVolume = 1f;

        public float GlobalVolume => _globalVolume;
        public float MusicVolume => _musicVolume;
        public float EffectsVolume => _effectsVolume;

        private void Awake()
        {
            LoadVolumes();
            ApplyVolumes();
        }

        public void LoadVolumes()
        {
            _globalVolume = PlayerPrefs.GetFloat(GlobalVolumeKey, 1f);
            _musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
            _effectsVolume = PlayerPrefs.GetFloat(EffectsVolumeKey, 1f);
        }

        public void SaveVolumes()
        {
            PlayerPrefs.SetFloat(GlobalVolumeKey, _globalVolume);
            PlayerPrefs.SetFloat(MusicVolumeKey, _musicVolume);
            PlayerPrefs.SetFloat(EffectsVolumeKey, _effectsVolume);
        }

        public void ApplyVolumes()
        {
            AudioListener.volume = _globalVolume;

            if (_musicSource != null)
                _musicSource.volume = _musicVolume * _globalVolume;

            if (_effectsSource != null)
                _effectsSource.volume = _effectsVolume * _globalVolume;
        }

        public void SetGlobalVolume(float value)
        {
            _globalVolume = Mathf.Clamp01(value);
            ApplyVolumes();
            SaveVolumes();
        }

        public void SetMusicVolume(float value)
        {
            _musicVolume = Mathf.Clamp01(value);
            ApplyVolumes();
            SaveVolumes();
        }

        public void SetEffectsVolume(float value)
        {
            _effectsVolume = Mathf.Clamp01(value);
            ApplyVolumes();
            SaveVolumes();
        }
    }
}
