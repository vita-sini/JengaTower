using System.Collections;
using UnityEngine;

namespace Effects
{
    public class RotatingPlatformEffect : BaseEffect
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed;

        protected override IEnumerator PlayEffect()
        {
            PlayEffectSound(loop: true);

            while (IsPlaying)
            {
                Camera.main.transform.RotateAround(_target.position, Vector3.up, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
