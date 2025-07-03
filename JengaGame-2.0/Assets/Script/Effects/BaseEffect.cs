using Gameplay;
using GameRoot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public abstract class BaseEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip _effectSound;
        [SerializeField] private GameEvents _gameEvents;

        private BlockRegistry _blockRegistry;
        private AudioSource _audioSource;
        private Coroutine _effectCoroutine;
        private bool _isPlaying = false;

        protected AudioSource AudioSource => _audioSource;
        protected bool IsPlaying => _isPlaying;
        protected AudioClip EffectSound => _effectSound;

        protected virtual void Awake()
        {
            _gameEvents.TurnEnd += Stop;
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            _gameEvents.TurnEnd -= Stop;
        }

        private void OnDestroy()
        {
            _gameEvents.TurnEnd -= Stop;
        }

        public void Execute()
        {
            if (_effectCoroutine != null)
                StopCoroutine(_effectCoroutine);

            _isPlaying = true;

            _effectCoroutine = StartCoroutine(PlayEffect());
        }

        public void InitEffect(BlockRegistry blockRegistry)
        {
            _blockRegistry = blockRegistry;
        }

        public virtual void Stop()
        {
            if (_effectCoroutine != null)
            {
                StopCoroutine(_effectCoroutine);
                _effectCoroutine = null;
            }

            _isPlaying = false;

            if (AudioSource != null)
                AudioSource.Stop();
        }

        protected void PlayEffectSound(bool loop = false)
        {
            if (AudioSource != null && EffectSound != null)
            {
                AudioSource.clip = EffectSound;
                AudioSource.loop = loop;
                AudioSource.Play();
            }
        }

        protected IEnumerable<GameObject> GetBlocks()
        {
            return _blockRegistry?.PlacedBlocks ?? new List<GameObject>();
        }

        protected abstract IEnumerator PlayEffect();
    }
}

