using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class SlipperyEffect : BaseEffect
    {
        [SerializeField] private PhysicMaterial _slipperyMaterial;

        private List<Collider> _affectedColliders = new();
        private PhysicMaterial _defaultMaterial;


        public override void Stop()
        {
            base.Stop();

            foreach (var collider in _affectedColliders)
            {
                if (collider != null)
                    collider.material = _defaultMaterial;
            }

            _affectedColliders.Clear();
        }

        protected override IEnumerator PlayEffect()
        {
            PlayEffectSound();

            foreach (var block in GetBlocks())
            {
                if (block.TryGetComponent(out Collider collider))
                {
                    if (_defaultMaterial == null)
                        _defaultMaterial = collider.sharedMaterial;

                    collider.material = _slipperyMaterial;
                    _affectedColliders.Add(collider);
                }
            }

            yield return null;
        }
    }
}
