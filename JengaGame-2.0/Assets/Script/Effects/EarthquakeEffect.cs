using Gameplay;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class EarthquakeEffect : BaseEffect
    {
        [SerializeField] private float _earthquakeForce;
        [SerializeField] private float _effectDuration;
        [SerializeField] private float _startTime = 3f;
        [SerializeField] private float _cameraShakeSpeed = 20f;
        [SerializeField] private float _cameraShakeAmplitude = 0.1f;
        [SerializeField] private float _shakeDirectionMin = -1f;
        [SerializeField] private float _shakeDirectionMax = 1f;
        [SerializeField] private float _shakeVerticalComponent = 0f;
        [SerializeField] private float _cameraShakeZAmplitude = 0f;

        protected override IEnumerator PlayEffect()
        {
            if (AudioSource && EffectSound)
            {
                AudioSource.clip = EffectSound;
                AudioSource.time = _startTime;
                AudioSource.Play();
            }

            var camera = Camera.main;
            Vector3 initialPosition = camera.transform.position;
            var cameraRotate = camera.GetComponent<CameraRotate>();
            if (cameraRotate) cameraRotate.enabled = false;

            float elapsed = 0f;

            while (elapsed < _effectDuration)
            {
                foreach (var block in GetBlocks())
                {
                    if (block.TryGetComponent(out Rigidbody rb))
                    {
                        Vector3 shakeDirection = new Vector3(
                            Random.Range(_shakeDirectionMin, _shakeDirectionMax),
                            _shakeVerticalComponent,
                            Random.Range(_shakeDirectionMin, _shakeDirectionMax)
                        ).normalized;

                        rb.AddForce(shakeDirection * _earthquakeForce, ForceMode.Impulse);
                    }
                }

                camera.transform.position = initialPosition + new Vector3(
                    Mathf.Sin(Time.time * _cameraShakeSpeed) * _cameraShakeAmplitude,
                    Mathf.Cos(Time.time * _cameraShakeSpeed) * _cameraShakeAmplitude,
                    _cameraShakeZAmplitude
                );

                elapsed += Time.deltaTime;
                yield return null;
            }

            camera.transform.position = initialPosition;
            if (cameraRotate) cameraRotate.enabled = true;

            Stop();
        }
    }
}
