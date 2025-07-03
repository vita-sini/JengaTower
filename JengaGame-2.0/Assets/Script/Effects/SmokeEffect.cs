using System.Collections;
using UnityEngine;

namespace Effects
{
    public class SmokeEffect : BaseEffect
    {
        [SerializeField] private ParticleSystem _smokeParticles;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _distanceFrontCamera;
        [SerializeField] private float _particleRotationX = -90f;
        [SerializeField] private float _particleRotationY = 0f;
        [SerializeField] private float _particleRotationZ = 0f;
        [SerializeField] private float _destroyDelaySeconds = 1f;

        private ParticleSystem _smokeParticlesInstance;
        private Camera _mainCamera;

        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main;
        }

        public override void Stop()
        {
            base.Stop();

            if (_smokeParticlesInstance != null)
            {
                _smokeParticlesInstance.Stop();
                Destroy(_smokeParticlesInstance.gameObject, _destroyDelaySeconds);
                _smokeParticlesInstance = null;
            }
        }

        private void UpdateParticlePosition()
        {
            if (_mainCamera == null || _smokeParticlesInstance == null) return;

            Vector3 cameraPos = _mainCamera.transform.position;
            Vector3 forward = _mainCamera.transform.forward;
            Vector3 targetPos = cameraPos + forward * _distanceFrontCamera;
            targetPos.y += _offsetY;

            _smokeParticlesInstance.transform.position = targetPos;
            _smokeParticlesInstance.transform.rotation = Quaternion.LookRotation(forward);
        }

        protected override IEnumerator PlayEffect()
        {
            if (_smokeParticlesInstance == null && _smokeParticles != null)
            {
                _smokeParticlesInstance = Instantiate(_smokeParticles);
                _smokeParticlesInstance.Play();
                var shape = _smokeParticlesInstance.shape;
                shape.rotation = new Vector3(_particleRotationX, _particleRotationY, _particleRotationZ);
            }

            PlayEffectSound(loop: true);

            while (IsPlaying)
            {
                UpdateParticlePosition();
                yield return null;
            }
        }
    }
}
