using UnityEngine;

namespace Gameplay
{
    public class BlockPusher : MonoBehaviour
    {
        [SerializeField] private float _pushForce = 3000f;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void PushBlockUnderCursor()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Rigidbody hitBlock = hit.rigidbody;

                if (hitBlock != null)
                {
                    Vector3 forceDirection = _mainCamera.transform.forward;
                    forceDirection.y = 0.5f; 
                    forceDirection.Normalize();
                    hitBlock.AddForce(forceDirection * _pushForce, ForceMode.Impulse);
                }
            }
        }
    }
}
