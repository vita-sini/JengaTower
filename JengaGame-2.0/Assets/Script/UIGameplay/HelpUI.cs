using UnityEngine;

namespace UIGameplay
{
    public class HelpUI : MonoBehaviour
    {
        [SerializeField] private GameObject _initialUIElement;
        [SerializeField] private GameObject[] _otherUIElements;

        public void CloseInitialUI()
        {
            _initialUIElement.SetActive(false);

            foreach (GameObject uiElement in _otherUIElements)
                if (uiElement != null)
                    uiElement.SetActive(true);

            Time.timeScale = 1f;
        }

        public void OpenInitialUI()
        {
            _initialUIElement.SetActive(true);

            foreach (GameObject uiElement in _otherUIElements)
                if (uiElement != null)
                    uiElement.SetActive(false);

            Time.timeScale = 0f;
        }
    }
}
