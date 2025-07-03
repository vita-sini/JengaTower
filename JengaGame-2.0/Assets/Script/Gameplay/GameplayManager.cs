using Cards;
using System.Collections;
using UIGameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameRoot;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private BlockManipulator _blockManipulator;
        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private Deck _deck;
        [SerializeField] private BlockSpawner _blockSpawner;
        [SerializeField] private BlockRegistry _blockRegistry;

        private float _previousMaxHeight;

        private void OnEnable()
        {
            _blockManipulator.BlockReleased += HandleBlockReleased;
        }

        private void OnDisable()
        {
            _blockManipulator.BlockReleased -= HandleBlockReleased;
        }

        private void HandleBlockReleased(Rigidbody block, Vector3 initialPosition)
        {
            _previousMaxHeight = GetCurrentTowerHeight();
            StartCoroutine(WaitForBlockToSettle(block, initialPosition));
        }

        private float GetCurrentTowerHeight()
        {
            if (_blockRegistry == null || _blockRegistry.PlacedBlocks.Count == 0)
                return 0f;

            float maxY = float.MinValue;

            foreach (GameObject block in _blockRegistry.PlacedBlocks)
            {
                if (block == null) continue;

                Renderer renderer = block.GetComponent<Renderer>();

                if (renderer != null)
                {
                    float topY = renderer.bounds.max.y;
                    if (topY > maxY)
                        maxY = topY;
                }
            }

            return maxY == float.MinValue ? 0f : maxY;
        }

        private IEnumerator WaitForBlockToSettle(Rigidbody block, Vector3 initialPosition)
        {
            ContactMonitor monitor = block.GetComponent<ContactMonitor>();

            if (monitor == null) yield break;

            while (!monitor.IsOnValidSurface())
            {
                yield return null;
            }

            BlockState blockState = block.GetComponent<BlockState>();

            if (blockState != null)
            {
                blockState.SetPlaced();
            }

            float blockTopY = block.GetComponent<Renderer>().bounds.max.y;

            if (blockTopY > _previousMaxHeight + 0.01f) 
            {
                _scoreUI.CalculateScore();

                if (SceneManager.GetActiveScene().name == Scenes.GAMEPLAYNEWCHALLENGES)
                    _deck.OnTurnEnd();

                _blockSpawner.UpdateTowerHeight(blockTopY);
            }

            _blockSpawner.SpawnBlock();
        }
    }
}
