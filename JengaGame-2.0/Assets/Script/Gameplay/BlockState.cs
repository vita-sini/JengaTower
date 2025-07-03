using UnityEngine;

namespace Gameplay
{
    public class BlockState : MonoBehaviour
    {
        private BlockStatus _currentState = BlockStatus.Base;
        private BlockRegistry _blockRegistry;

        public BlockStatus CurrentState => _currentState;

        private void OnDisable()
        {
            _blockRegistry?.Unregister(gameObject);
        }

        public void Initialize(BlockRegistry registry)
        {
            _blockRegistry = registry;
        }

        public void SetSpawning() => _currentState = BlockStatus.Spawning;

        public void SetPlaced()
        {
            _currentState = BlockStatus.Placed;
            _blockRegistry?.Register(gameObject);
        }
    }
}
