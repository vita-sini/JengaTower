using UnityEngine;
using GameRoot;

namespace Gameplay
{
    public class ProjectionGhost : MonoBehaviour
    {
        private Transform _ghostInstance;
        private Rigidbody _targetBlock;
        private Material _ghostMaterial;

        private void Update()
        {
            if (_targetBlock == null || _ghostInstance == null) return;

            Vector3 origin = _targetBlock.position;
            Vector3 direction = Vector3.down;

            int mask = ~LayerMask.GetMask(LayerNames.IgnoreRaycast);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, 100f, mask))
            {
                Vector3 newPos = hit.point;
                _ghostInstance.position = new Vector3(origin.x, newPos.y, origin.z);
                _ghostInstance.rotation = _targetBlock.rotation;
            }
        }

        public void Initialize(Rigidbody target, Material material)
        {
            _targetBlock = target;
            _ghostMaterial = material;

            _ghostInstance = Instantiate(target.gameObject).transform;
            _ghostInstance.name = "GhostProjection";

            foreach (var rb in _ghostInstance.GetComponentsInChildren<Rigidbody>())
                Destroy(rb);

            foreach (var collider in _ghostInstance.GetComponentsInChildren<Collider>())
                Destroy(collider);

            foreach (var renderer in _ghostInstance.GetComponentsInChildren<Renderer>())
                renderer.material = _ghostMaterial;

            _ghostInstance.gameObject.layer = LayerMask.NameToLayer(LayerNames.IgnoreRaycast);

            MaterialApplier applier = _ghostInstance.GetComponent<MaterialApplier>();

            if (applier != null)
                Destroy(applier);
        }

        public void DestroyGhost()
        {
            if (_ghostInstance != null)
                Destroy(_ghostInstance.gameObject);
        }
    }
}
