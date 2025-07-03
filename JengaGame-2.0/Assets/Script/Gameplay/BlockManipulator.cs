using GameRoot;
using System;
using UnityEngine;

namespace Gameplay
{
    public class BlockManipulator : MonoBehaviour
    {
        [SerializeField] private Material _ghostMaterial;
        [SerializeField] private BlockSpawner _blockSpawner;
        [SerializeField] private GameEvents _gameEvents;

        private ProjectionGhost _projectionGhost;
        private Pick _pick;
        private Release _release;
        private BlockRotate _rotate;
        private Movement _movement;
        private MouseWorldPosition _mouseWorldPosition;

        private Rigidbody _selectedBlock;
        private Vector3 _offset;
        private Vector3 _initialBlockPosition;

        public event Action<Rigidbody, Vector3> BlockReleased;

        public bool IsBlockHeld { get; private set; }

        private void Awake()
        {
            _mouseWorldPosition = new MouseWorldPosition();
            _pick = new Pick(_mouseWorldPosition);
            _release = new Release();
            _rotate = new BlockRotate();
            _movement = new Movement(_mouseWorldPosition);
            _projectionGhost = gameObject.AddComponent<ProjectionGhost>();
        }

        public void TryStartManipulation()
        {
            _pick.Select(ref _selectedBlock, ref _offset, ref _initialBlockPosition);

            if (_selectedBlock != null)
            {
                if (_blockSpawner.CurrentSpawnedBlock != null && _selectedBlock.gameObject == _blockSpawner.CurrentSpawnedBlock)
                    _blockSpawner.ReleaseBlock();

                IsBlockHeld = true;
                _projectionGhost.Initialize(_selectedBlock, _ghostMaterial);
                _gameEvents.OnTriggerCameraControlsLock();
            }
        }

        public void UpdateManipulation()
        {
            if (!IsBlockHeld) return;

            _movement.MoveMouse(_selectedBlock, _offset);
            _rotate.Twist(_selectedBlock);
        }

        public void EndManipulation()
        {
            if (!IsBlockHeld) return;

            _release.FreeBlock(_selectedBlock);

            BlockReleased?.Invoke(_selectedBlock, _initialBlockPosition);

            _selectedBlock = null;
            IsBlockHeld = false;
            _projectionGhost.DestroyGhost();
            _gameEvents.OnTriggerCameraControlsUnlock();
        }
    }
}
