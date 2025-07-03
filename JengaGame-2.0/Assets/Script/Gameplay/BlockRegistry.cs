using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BlockRegistry : MonoBehaviour
    {
        private readonly List<GameObject> _placedBlocks = new();

        public IReadOnlyList<GameObject> PlacedBlocks => _placedBlocks;

        public void Register(GameObject block)
        {
            if (!_placedBlocks.Contains(block))
                _placedBlocks.Add(block);
        }

        public void Unregister(GameObject block)
        {
            if (_placedBlocks.Contains(block))
                _placedBlocks.Remove(block);
        }
    }
}
