using GameRoot;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cards
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cardDescriptionText;
        [SerializeField] private GameObject _cardPanel;
        [SerializeField] private float _cardDisplayDuration = 3f;
        [SerializeField] private Localization _localization;

        public void ShowCard(Card card)
        {
            _cardDescriptionText.text = card.GetDescription(_localization);
            _cardPanel.SetActive(true);
            StartCoroutine(HideCardAfterDelay(_cardDisplayDuration));
        }

        private IEnumerator HideCardAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            _cardPanel.SetActive(false);
        }
    }
}
