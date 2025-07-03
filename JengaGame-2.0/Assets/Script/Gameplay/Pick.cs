using UnityEngine;

namespace Gameplay
{
    public class Pick
    {
        private MouseWorldPosition _mouseWorldPosition;
        private Camera _mainCamera;

        public Pick(MouseWorldPosition mouseWorldPosition)
        {
            _mainCamera = Camera.main;
            _mouseWorldPosition = mouseWorldPosition;
        }

        public void Select(ref Rigidbody selectedBlock, ref Vector3 offset, ref Vector3 initialBlockPosition)
        {
            if (TryGetRaycastHitBlock(out RaycastHit hit, out Rigidbody rb, out ContactMonitor monitor))
            {
                if (CanBlockBeSelected(rb, monitor, hit))
                {
                    selectedBlock = rb;
                    initialBlockPosition = selectedBlock.position;

                    Plane movementPlane = new Plane(Vector3.up, selectedBlock.position);
                    Vector3 mouseWorldPos = _mouseWorldPosition.GetMouseWorldPosition(movementPlane);

                    offset = selectedBlock.position - mouseWorldPos;

                    selectedBlock.constraints = RigidbodyConstraints.FreezeRotation;
                    selectedBlock.GetComponent<ContactMonitor>().ClearContacts(); ;
                }
            }
        }

        private bool CanBlockBeSelected(Rigidbody rb, ContactMonitor monitor, RaycastHit hit)
        {
            if (rb == null) return false;

            BlockState blockState = rb.GetComponent<BlockState>();

            if (blockState == null) return false;

            return blockState.CurrentState == BlockStatus.Spawning ||
           blockState.CurrentState == BlockStatus.Placed;
        }

        private bool TryGetRaycastHitBlock(out RaycastHit hit, out Rigidbody rb, out ContactMonitor monitor)
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                rb = hit.collider.GetComponent<Rigidbody>();
                monitor = hit.collider.GetComponent<ContactMonitor>();
                return true;
            }

            rb = null;
            monitor = null;
            return false;
        }
    }
}
