using System.Collections;
using UnityEngine;

namespace Effects
{
    public class WindEffect : BaseEffect
    {
        [SerializeField] private ParticleSystem _windParticlesPrefab;
        [SerializeField] private Transform _particleSpawnPoint;
        [SerializeField] private float _particleTravelSpeed = 10f;
        [SerializeField] private float _gustOscillationFrequency = 0.01f;
        [SerializeField] private float _windForce;
        [SerializeField] private float _effectDuration;

        private ParticleSystem _windParticlesInstance;

        protected override IEnumerator PlayEffect()
        {
            if (_windParticlesPrefab)
            {
                _windParticlesInstance = Instantiate(_windParticlesPrefab, _particleSpawnPoint.position, Quaternion.LookRotation(Vector3.up));
                _windParticlesInstance.Play();
            }

            float elapsed = 0f;

            while (elapsed < _effectDuration)
            {
                if (_windParticlesInstance != null)
                    _windParticlesInstance.transform.Translate(Vector3.right * Time.deltaTime * _particleTravelSpeed);

                foreach (GameObject block in GetBlocks())
                {
                    if (block.TryGetComponent(out Rigidbody rb))
                    {
                        float strength = _windForce * Mathf.Sin(Time.time * _gustOscillationFrequency);
                        rb.AddForce(Vector3.right * strength, ForceMode.Force);
                    }
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            if (_windParticlesInstance)
            {
                _windParticlesInstance.Stop();
                Destroy(_windParticlesInstance.gameObject);
            }

            Stop();
        }
    }
}
