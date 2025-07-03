using UnityEngine;

namespace Gameplay
{
    public class Movement
    {
        private float _maxMoveSpeed = 17f;
        private float _verticalMoveSpeed = 15f;

        private MouseWorldPosition _mouseWorldPosition;

        public Movement(MouseWorldPosition mouseWorldPosition)
        {
            _mouseWorldPosition = mouseWorldPosition;
        }

        public void MoveMouse(Rigidbody selectedBlock, Vector3 offset)
        {
            Plane movementPlane = new Plane(Vector3.up, selectedBlock.position);
            Vector3 mouseTargetPosition = _mouseWorldPosition.GetMouseWorldPosition(movementPlane) + offset;

            Vector3 horizontalTarget = new Vector3(mouseTargetPosition.x, selectedBlock.position.y, mouseTargetPosition.z);
            Vector3 horizontalDirection = horizontalTarget - selectedBlock.position;
            Vector3 horizontalVelocity = horizontalDirection / Time.fixedDeltaTime;

            if (horizontalVelocity.magnitude > _maxMoveSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * _maxMoveSpeed;
            }

            float verticalInput = 0f;
            if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
            else if (Input.GetKey(KeyCode.S)) verticalInput = -1f;

            float verticalVelocity = verticalInput * _verticalMoveSpeed;

            Vector3 finalVelocity = new Vector3(
                horizontalVelocity.x,
                verticalVelocity,
                horizontalVelocity.z
            );

            if (selectedBlock.isKinematic)
                selectedBlock.isKinematic = false;

            selectedBlock.velocity = finalVelocity;
        }
    }
}
