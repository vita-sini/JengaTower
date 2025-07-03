using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class ExplosiveEffect : BaseEffect
    {
        [SerializeField] private ParticleSystem _explosionPrefab;
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDelay = 2f;

        private Rigidbody _targetBlock;

        protected override IEnumerator PlayEffect()
        {
            List<Rigidbody> candidates = new();

            foreach (var block in GetBlocks())
            {
                if (block.TryGetComponent(out BlockState state) &&
                    block.TryGetComponent(out Rigidbody rb) &&
                    state.CurrentState == BlockStatus.Placed)
                {
                    candidates.Add(rb);
                }
            }

            if (candidates.Count > 0)
            {
                _targetBlock = candidates[Random.Range(0, candidates.Count)];


                yield return new WaitForSeconds(_explosionDelay);

                if (_targetBlock != null)
                {
                    Vector3 position = _targetBlock.transform.position;

                    Collider[] affected = Physics.OverlapSphere(position, _explosionRadius);

                    foreach (var col in affected)
                    {
                        if (col.TryGetComponent(out Rigidbody rb) && rb != _targetBlock)
                            rb.AddExplosionForce(_explosionForce, position, _explosionRadius);
                    }

                    if (_explosionPrefab)
                        Instantiate(_explosionPrefab, position, Quaternion.identity);

                    if (AudioSource && EffectSound)
                        AudioSource.PlayOneShot(EffectSound);

                    Destroy(_targetBlock.gameObject);
                }
            }

            _targetBlock = null;
            yield break;
        }
    }
}
