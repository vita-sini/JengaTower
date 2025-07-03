using UnityEngine;

namespace Gameplay
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private BlockPool _blockPool;
        [SerializeField] private BlockRegistry _blockRegistry;

        private float _currentTowerHeight;

        public GameObject CurrentSpawnedBlock { get; private set; }

        private void Start()
        {
            _currentTowerHeight = _spawnPoint.position.y;
            SpawnBlock();
        }

        public void SpawnBlock()
        {
            if (CurrentSpawnedBlock != null) return;

            GameObject block = _blockPool.GetBlock();

            if (block == null) return;

            Vector3 spawnPos = _spawnPoint.position;

            spawnPos.y = Mathf.Max(_spawnPoint.position.y, _currentTowerHeight + 2f);

            block.transform.position = spawnPos;
            block.transform.rotation = Quaternion.identity;

            Rigidbody rb = block.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            var blockState = block.GetComponent<BlockState>();

            if (blockState != null)
            {
                blockState.Initialize(_blockRegistry); 
                blockState.SetSpawning();
            }

            CurrentSpawnedBlock = block;
        }

        public void ReleaseBlock()
        {
            if (CurrentSpawnedBlock == null) return;

            Rigidbody rb = CurrentSpawnedBlock.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            CurrentSpawnedBlock = null;
        }

        public void UpdateTowerHeight(float newHeight)
        {
            if (newHeight > _currentTowerHeight)
                _currentTowerHeight = newHeight;
        }
    }
}
