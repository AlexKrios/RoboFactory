using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localization;
using RoboFactory.General.Order;
using RoboFactory.Utils;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Order
{
    public class ItemCellView : MonoBehaviour
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly OrderManager _orderManager;

        [SerializeField] private ProductGroup _group;
        
        [Space]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _prize;
        [SerializeField] private Button _button;
        [SerializeField] private Image _bar;
        [SerializeField] private TMP_Text _count;
        
        public ProductGroup Group => _group;
        
        private OrderObject _orderData;
        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _button.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        public void SetData(OrderObject order)
        {
            _orderData = order;

            SetView();
        }

        private async void SetView()
        {
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(_orderData.Part.Data.IconRef);
            SetTitleText();
            SetPrizeText();
            _button.interactable = _orderManager.IsEnoughParts(_orderData);
            SetBarProgress();
            SetCountText();
        }

        private void SetTitleText()
        {
            var titleKey = $"{_orderData.Key}_title";
            var text = _localizationService.GetLanguageValue(titleKey);
            var item = _localizationService.GetLanguageValue(_orderData.Part.Data.Key);
            _title.text = string.Format(text, item);
        }

        private void SetPrizeText()
        {
            var cost = _productsManager.GetProduct(_orderData.Part.Data.Key).Recipe.Cost;
            _prize.text = StringUtil.PriceShortFormat(_orderData.Part.Count * cost);
        }

        private void SetBarProgress()
        {
            var itemKey = _orderData.Part.Data.Key;
            var currentCount = _productsManager.GetProduct(itemKey).Count;
            var step = 1f / _orderData.Part.Count;
            _bar.fillAmount = step * currentCount;

            if (_bar.fillAmount > 1)
                _bar.fillAmount = 1;
        }
        
        private void SetCountText()
        {
            var itemKey = _orderData.Part.Data.Key;
            var currentCount = _productsManager.GetProduct(itemKey).Count;
            _count.text = $"{currentCount}/{_orderData.Part.Count}";
        }

        private void Click()
        {
            _orderData.IsComplete = true;
            _orderManager.RemoveItems(_orderData);
            _orderManager.CollectMoney(_orderData);
            
            SetView();
        }
    }
}