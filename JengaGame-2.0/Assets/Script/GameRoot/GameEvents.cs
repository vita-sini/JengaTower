using System;
using UnityEngine;

namespace GameRoot
{
    public class GameEvents : MonoBehaviour
    {
        public event Action TurnEnd;
        public event Action GameOver;

        public event Action CameraControlsLock;
        public event Action CameraControlsUnlock;

        public void OnTriggerCameraControlsLock() => CameraControlsLock?.Invoke();
        public void OnTriggerCameraControlsUnlock() => CameraControlsUnlock?.Invoke();
        public void OnInvokeTurnEnd() => TurnEnd?.Invoke();
        public void OnInvokeGameOver() => GameOver?.Invoke();
    }
}
