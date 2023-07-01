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

        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly OrderManager _orderManager;

        #endregion

        #region Components

        [SerializeField] private ProductGroup group;
        
        [Space]
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text prize;
        [SerializeField] private Button button;
        [SerializeField] private Image bar;
        [SerializeField] private TMP_Text count;
        
        public ProductGroup Group => group;

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
            
            button.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
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
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(_orderData.Part.data.IconRef);
            SetTitleText();
            SetPrizeText();
            button.interactable = _orderManager.IsEnoughParts(_orderData);
            SetBarProgress();
            SetCountText();
        }

        private void SetTitleText()
        {
            var titleKey = $"{_orderData.Key}_title";
            var text = _localisationController.GetLanguageValue(titleKey);
            var item = _localisationController.GetLanguageValue(_orderData.Part.data.Key);
            title.text = string.Format(text, item);
        }

        private void SetPrizeText()
        {
            var cost = _productsManager.GetProduct(_orderData.Part.data.Key).Recipe.Cost;
            prize.text = StringUtil.PriceShortFormat(_orderData.Part.count * cost);
        }

        private void SetBarProgress()
        {
            var itemKey = _orderData.Part.data.Key;
            var currentCount = _productsManager.GetProduct(itemKey).Count;
            var step = 1f / _orderData.Part.count;
            bar.fillAmount = step * currentCount;

            if (bar.fillAmount > 1)
                bar.fillAmount = 1;
        }
        
        private void SetCountText()
        {
            var itemKey = _orderData.Part.data.Key;
            var currentCount = _productsManager.GetProduct(itemKey).Count;
            count.text = $"{currentCount}/{_orderData.Part.count}";
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