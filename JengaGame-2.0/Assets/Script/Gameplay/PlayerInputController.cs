using UIGameplay;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private BlockManipulator _blockManipulator;
        [SerializeField] private BlockPusher _blockPusher;
        [SerializeField] private PauseMenu _pauseMenu; 

        private void Update()
        {
            if (_pauseMenu.IsPaused)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _blockManipulator.TryStartManipulation();
            }
            else if (Input.GetMouseButton(0))
            {
                _blockManipulator.UpdateManipulation();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _blockManipulator.EndManipulation();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _blockPusher.PushBlockUnderCursor();
            }
        }
    }
}
