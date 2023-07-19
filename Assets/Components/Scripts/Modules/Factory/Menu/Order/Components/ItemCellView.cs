using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localisation;
using RoboFactory.General.Order;
using RoboFactory.Utils;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Order
{
    [AddComponentMenu("Scripts/Factory/Menu/Order/Item Cell View")]
    public class ItemCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly OrderManager _orderManager;

        #endregion

        #region Components

        [SerializeField] private ProductGroup _group;
        
        [Space]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _prize;
        [SerializeField] private Button _button;
        [SerializeField] private Image _bar;
        [SerializeField] private TMP_Text _count;
        
        public ProductGroup Group => _group;

        #endregion
        
        #region Variables

        private OrderObject _orderData;
        private CompositeDisposable _disposable;
        
        private AssetReference _iconRef;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            _button.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion

        public void SetData(OrderObject order)
        {
            _orderData = order;

            SetView();
        }

        private async void SetView()
        {
            _icon.sprite = await addressableService.LoadAssetAsync<Sprite>(_orderData.Part.data.IconRef);
            SetTitleText();
            SetPrizeText();
            _button.interactable = _orderManager.IsEnoughParts(_orderData);
            SetBarProgress();
            SetCountText();
        }

        private void SetTitleText()
        {
            var titleKey = $"{_orderData.Key}_title";
            var text = localizationController.GetLanguageValue(titleKey);
            var item = localizationController.GetLanguageValue(_orderData.Part.data.Key);
            _title.text = string.Format(text, item);
        }

        private void SetPrizeText()
        {
            var cost = _productsManager.GetProduct(_orderData.Part.data.Key).Recipe.Cost;
            _prize.text = StringUtil.PriceShortFormat(_orderData.Part.count * cost);
        }

        private void SetBarProgress()
        {
            var itemKey = _orderData.Part.data.Key;
            var currentCount = _productsManager.GetProduct(itemKey).Count;
            var step = 1f / _orderData.Part.count;
            _bar.fillAmount = step * currentCount;

            if (_bar.fillAmount > 1)
                _bar.fillAmount = 1;
        }
        
        private void SetCountText()
        {
            var itemKey = _orderData.Part.data.Key;
            var currentCount = _productsManager.GetProduct(itemKey).Count;
            _count.text = $"{currentCount}/{_orderData.Part.count}";
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