using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class GhostEffect : BaseEffect
    {
        [SerializeField] private Material _ghostMaterial;
        [SerializeField] private int _count;

        private List<Renderer> _affected = new();
        private List<Material> _originalMats = new();

        public override void Stop()
        {
            base.Stop();

            for (int i = 0; i < _affected.Count; i++)
            {
                if (_affected[i] != null)
                    _affected[i].material = _originalMats[i];
            }

            _affected.Clear();
            _originalMats.Clear();
        }

        protected override IEnumerator PlayEffect()
        {
            PlayEffectSound();

            foreach (var block in GetBlocks())
            {
                if (_affected.Count >= _count)
                    break;

                if (block.TryGetComponent(out Renderer renderer))
                {
                    _affected.Add(renderer);
                    _originalMats.Add(renderer.material);
                    renderer.material = _ghostMaterial;
                }
            }

            yield return null;
        }
    }
}
