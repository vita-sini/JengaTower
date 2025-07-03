using UnityEngine;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private AudioClip[] _soundEffects;

        private void Start()
        {
            _audioManager.ApplyVolumes();
        }

        public void PlaySoundEffect(int index)
        {
            if (index < 0 || index >= _soundEffects.Length)
                return;

            float volume = _audioManager.EffectsVolume * _audioManager.GlobalVolume;
            AudioSource.PlayClipAtPoint(_soundEffects[index], Camera.main.transform.position, volume);
        }
    }
}
