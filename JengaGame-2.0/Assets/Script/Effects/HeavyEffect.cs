using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class HeavyEffect : BaseEffect
    {
        [SerializeField] private int _affectedCount;
        [SerializeField] private float _heavyMass;

        private List<Rigidbody> _affected = new();
        private Dictionary<Rigidbody, float> _originalMasses = new();

        public override void Stop()
        {
            base.Stop();

            foreach (var rb in _affected)
            {
                if (rb != null && _originalMasses.ContainsKey(rb))
                    rb.mass = _originalMasses[rb];
            }

            _affected.Clear();
            _originalMasses.Clear();
        }

        protected override IEnumerator PlayEffect()
        {
            PlayEffectSound();

            foreach (var block in GetBlocks())
            {
                if (_affected.Count >= _affectedCount)
                    break;

                if (block.TryGetComponent(out Rigidbody rb))
                {
                    _originalMasses[rb] = rb.mass;
                    rb.mass = _heavyMass;
                    _affected.Add(rb);
                }
            }

            yield return null;
        }
    }
}
