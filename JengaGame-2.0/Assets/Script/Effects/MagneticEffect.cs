using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameRoot;

namespace Effects
{
    public class MagneticEffect : BaseEffect
    {
        [SerializeField] private int _numberOfMagneticBlocks;
        [SerializeField] private int _strengthGap;
        [SerializeField] private float _rayDistance = 10f;

        private readonly List<FixedJoint> _joints = new();

        public override void Stop()
        {
            base.Stop();

            foreach (var joint in _joints)
            {
                if (joint)
                    Destroy(joint);
            }

            _joints.Clear();
        }

        private void TryCreateJoint(Rigidbody rb)
        {
            Vector3[] directions = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };
            Vector3 direction = directions[Random.Range(0, directions.Length)];

            if (Physics.Raycast(rb.position, direction, out RaycastHit hit, _rayDistance))
            {
                if (hit.collider.CompareTag(Tags.Block) && hit.collider.TryGetComponent(out Rigidbody otherRb))
                {
                    FixedJoint joint = rb.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = otherRb;
                    joint.breakForce = _strengthGap;
                    _joints.Add(joint);
                }
            }
        }

        protected override IEnumerator PlayEffect()
        {
            PlayEffectSound(loop: true);

            var blocks = GetBlocks().ToArray();

            if (blocks.Length == 0)
            {
                yield break;
            }

            for (int i = 0; i < _numberOfMagneticBlocks; i++)
            {
                var block = blocks[Random.Range(0, blocks.Length)];
                if (block.TryGetComponent(out Rigidbody rb))
                    TryCreateJoint(rb);
            }

            yield return null;
        }
    }
}
