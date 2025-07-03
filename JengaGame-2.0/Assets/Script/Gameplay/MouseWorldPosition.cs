using UnityEngine;

namespace Gameplay
{
    public class MouseWorldPosition
    {
        private Camera _mainCamera;

        public MouseWorldPosition()
        {
            _mainCamera = Camera.main;
        }

        public Vector3 GetMouseWorldPosition(Plane movementPlane)
        {
            Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (movementPlane.Raycast(mouseRay, out float distance))
                return mouseRay.GetPoint(distance);

            return Vector3.zero;
        }
    }
}
