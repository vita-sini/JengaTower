using GameRoot;
using UnityEngine;

namespace Gameplay
{
    public class GameOverTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvents _gameEvents;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Block))
                _gameEvents.OnInvokeGameOver();
        }
    }
}
