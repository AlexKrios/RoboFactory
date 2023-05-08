using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Localisation;
using Modules.General.Order;
using Modules.General.Order.Models.Object;
using Modules.General.Save;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Order.Components
{
    [AddComponentMenu("Scripts/Factory/Menu/Order/Item Cell View")]
    public class ItemCellView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly IOrderController _orderController;
        [Inject] private readonly ISaveController _saveController;

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
            icon.sprite = await AssetsController.LoadAsset<Sprite>(_orderData.part.data.IconRef);
            SetTitleText();
            SetPrizeText();
            button.interactable = _orderController.IsEnoughParts(_orderData);
            SetBarProgress();
            SetCountText();
        }

        private void SetTitleText()
        {
            var titleKey = $"{_orderData.key}_title";
            var text = _localisationController.GetLanguageValue(titleKey);
            var item = _localisationController.GetLanguageValue(_orderData.part.data.Key);
            title.text = string.Format(text, item);
        }

        private void SetPrizeText()
        {
            var cost = _productsController.GetProduct(_orderData.part.data.Key).Recipe.Cost;
            prize.text = StringUtil.PriceShortFormat(_orderData.part.count * cost);
        }

        private void SetBarProgress()
        {
            var itemKey = _orderData.part.data.Key;
            var currentCount = _productsController.GetProduct(itemKey).Count;
            var step = 1f / _orderData.part.count;
            bar.fillAmount = step * currentCount;

            if (bar.fillAmount > 1)
                bar.fillAmount = 1;
        }
        
        private void SetCountText()
        {
            var itemKey = _orderData.part.data.Key;
            var currentCount = _productsController.GetProduct(itemKey).Count;
            count.text = $"{currentCount}/{_orderData.part.count}";
        }

        private void Click()
        {
            _orderData.isComplete = true;
            _orderController.RemoveItems(_orderData);
            _orderController.CollectMoney(_orderData);
            
            SetView();
            
            _saveController.SaveOrders();
        }
    }
}