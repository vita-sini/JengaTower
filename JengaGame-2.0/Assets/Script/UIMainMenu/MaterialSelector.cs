using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.Pay;

namespace UIMainMenu
{
    public class MaterialSelector : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Material[] _materials;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _textPrice;
        [SerializeField] private string _paidProductId;
        [SerializeField] private int _paidMaterialIndex;
        [SerializeField] private int _freeMaterialIndex;

        private int _currentIndex;

        private void Awake()
        {
            _leftButton.onClick.AddListener(OnLeft);
            _rightButton.onClick.AddListener(OnRight);
            _selectButton.onClick.AddListener(OnSelect);
            _buyButton.onClick.AddListener(OnBuy);
        }

        private void OnEnable()
        {
            YandexGame.GetPaymentsEvent += OnPayments;
            YandexGame.PurchaseSuccessEvent += OnPurchaseSuccess;

            _currentIndex = YandexGame.savesData.selectedMaterialIndex;

            ShowImage(_currentIndex);
            UpdateSelectBuyButtons();

            YandexGame.GetPayments();
        }

        private void OnDisable()
        {
            YandexGame.GetPaymentsEvent -= OnPayments;
            YandexGame.PurchaseSuccessEvent -= OnPurchaseSuccess;
        }

        private void OnLeft()
        {
            _currentIndex = (_currentIndex - 1 + _images.Length) % _images.Length;
            ShowImage(_currentIndex);
        }

        private void OnRight()
        {
            _currentIndex = (_currentIndex + 1) % _images.Length;
            ShowImage(_currentIndex);
        }

        private void ShowImage(int index)
        {
            for (int i = 0; i < _images.Length; i++)
                _images[i].gameObject.SetActive(i == index);

            UpdateSelectBuyButtons();
        }

        private void OnSelect()
        {
            YandexGame.savesData.selectedMaterialIndex = _currentIndex;
            YandexGame.SaveProgress();
        }

        private void OnBuy() { YandexGame.BuyPayments(_paidProductId); }

        private void OnPurchaseSuccess(string productId)
        {
            if (!YandexGame.savesData.purchasedProductIds.Contains(productId))
            {
                YandexGame.savesData.purchasedProductIds.Add(productId);
                YandexGame.SaveProgress();
            }

            UpdateSelectBuyButtons();
        }

        private void OnPayments()
        {
            if (YandexGame.PurchaseByID(_paidProductId) != null &&
                !YandexGame.savesData.purchasedProductIds.Contains(_paidProductId))
            {
                YandexGame.savesData.purchasedProductIds.Add(_paidProductId);
                YandexGame.SaveProgress();
            }

            UpdateSelectBuyButtons();
        }

        private void UpdateSelectBuyButtons()
        {
            bool isBought = YandexGame.savesData.purchasedProductIds.Contains(_paidProductId);
            bool isFree = _currentIndex == _freeMaterialIndex;
            bool canChoose = isFree || isBought;
            bool needBuy = !isFree && !isBought;

            _selectButton.gameObject.SetActive(canChoose);

            _buyButton.gameObject.SetActive(needBuy);

            _textPrice.SetActive(needBuy);

            if (needBuy)
            {
                Purchase purchase = YandexGame.PurchaseByID(_paidProductId);

                _priceText.text = purchase != null
                    ? $"{purchase.priceValue} {purchase.priceCurrencyCode}"
                    : "—";
            }
        }
    }
}
