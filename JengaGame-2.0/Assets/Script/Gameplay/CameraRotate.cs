using GameRoot;
using UnityEngine;

namespace Gameplay
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _rotationSpeed = 50f;
        [SerializeField] private float _scrollSpeed = 10f;
        [SerializeField] private float _minHeight = 4f;
        [SerializeField] private float _maxHeight = 60f;
        [SerializeField] private GameEvents _gameEvents;

        private bool _isLocked = false;

        private void OnEnable()
        {
            _gameEvents.CameraControlsLock += Lock;
            _gameEvents.CameraControlsUnlock += Unlock;
        }

        private void OnDisable()
        {
            _gameEvents.CameraControlsLock -= Lock;
            _gameEvents.CameraControlsUnlock -= Unlock;
        }

        private void Update()
        {
            if (_isLocked)
                return;

            if (Input.GetKey(KeyCode.Q))
                RotateAroundTarget(-1);

            if (Input.GetKey(KeyCode.E))
                RotateAroundTarget(1);

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0f)
                MoveCameraVertically(scroll);
        }

        private void Lock() => _isLocked = true;
        private void Unlock() => _isLocked = false;

        private void RotateAroundTarget(float direction)
        {
            transform.RotateAround(_target.position, Vector3.up, direction * _rotationSpeed * Time.deltaTime);
        }

        private void MoveCameraVertically(float scrollAmount)
        {
            Vector3 position = transform.position;
            float newY = position.y - scrollAmount * _scrollSpeed;
            position.y = Mathf.Clamp(newY, _minHeight, _maxHeight);
            transform.position = position;
        }
    }
}
