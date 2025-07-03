using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class GlitchEffect : BaseEffect
    {
        [SerializeField] private int _affectedCount;
        [SerializeField] private float _dragMultiplier;
        [SerializeField] private float _angularDragMultiplier;

        private List<Rigidbody> _affected = new();
        private Dictionary<Rigidbody, float> _origDrags = new();
        private Dictionary<Rigidbody, float> _origAngularDrags = new();

        public override void Stop()
        {
            base.Stop();

            foreach (var rb in _affected)
            {
                rb.drag = _origDrags[rb];
                rb.angularDrag = _origAngularDrags[rb];
            }

            _affected.Clear();
            _origDrags.Clear();
            _origAngularDrags.Clear();
        }

        protected override IEnumerator PlayEffect()
        {
            PlayEffectSound();

            foreach (var block in GetBlocks())
            {
                if (_affected.Count >= _affectedCount)
                    break;

                if (block.TryGetComponent(out Rigidbody rb) && !_affected.Contains(rb))
                {
                    _origDrags[rb] = rb.drag;
                    _origAngularDrags[rb] = rb.angularDrag;
                    rb.drag *= _dragMultiplier;
                    rb.angularDrag *= _angularDragMultiplier;
                    _affected.Add(rb);
                }
            }

            yield return null;
        }
    }
}
