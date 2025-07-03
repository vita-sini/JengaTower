using UnityEngine;

namespace Gameplay
{
    public class BlockRotate
    {
        private float _rotationAnglePositive = 15f;
        private float _rotationAngleNegative = -15f;

        public void Twist(Rigidbody selectedBlock)
        {
            if (Input.GetKeyDown(KeyCode.A))
                selectedBlock.MoveRotation(selectedBlock.rotation * Quaternion.Euler(0, _rotationAnglePositive, 0));

            if (Input.GetKeyDown(KeyCode.D))
                selectedBlock.MoveRotation(selectedBlock.rotation * Quaternion.Euler(0, _rotationAngleNegative, 0));
        }
    }
}
