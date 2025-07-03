using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class AudioSettingsUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Slider _globalVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;

        [Header("Audio Manager")]
        [SerializeField] private AudioManager _audio;

        private void Start()
        {
            if (_audio == null)
            {
                return;
            }

            _globalVolumeSlider.value = _audio.GlobalVolume;
            _musicVolumeSlider.value = _audio.MusicVolume;
            _effectsVolumeSlider.value = _audio.EffectsVolume;

            _globalVolumeSlider.onValueChanged.AddListener(OnGlobalVolumeChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        }


        private void OnDestroy()
        {
            _globalVolumeSlider.onValueChanged.RemoveListener(OnGlobalVolumeChanged);
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            _effectsVolumeSlider.onValueChanged.RemoveListener(OnEffectsVolumeChanged);
        }

        private void OnGlobalVolumeChanged(float volume)
        {
            _audio.SetGlobalVolume(volume);
            _audio.SaveVolumes();
        }

        private void OnMusicVolumeChanged(float volume)
        {
            _audio.SetMusicVolume(volume);
            _audio.SaveVolumes();
        }

        private void OnEffectsVolumeChanged(float volume)
        {
            _audio.SetEffectsVolume(volume);
            _audio.SaveVolumes();
        }
    }
}
