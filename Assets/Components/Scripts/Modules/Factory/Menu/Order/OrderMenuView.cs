using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RoboFactory.General.Localisation;
using RoboFactory.General.Order;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.Utils;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Order
{
    [AddComponentMenu("Scripts/Factory/Menu/Order/Order Menu View")]
    public class OrderMenuView : PopupBase
    {
        private const string TimerTemplate = "HH:mm:ss";
        
        #region Zenject

        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly OrderManager _orderManager;
        [Inject] private readonly OrderMenuFactory _orderManagerFactory;

        #endregion

        #region Components
        
        [SerializeField] private Button _upgrade;
        
        [Space]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _timer; 
        [SerializeField] private List<ItemCellView> _orders;
        
        [Space] 
        [SerializeField] private Transform _parent;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _upgrade.OnClickAsObservable().Subscribe(_ => OnUpgradeClick()).AddTo(Disposable);
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            
            _title.text = localizationController.GetLanguageValue(LocalizationKeys.OrderMenuTitleKey);

            SetData();
            StartRefreshTimer();
        }

        private async void StartRefreshTimer()
        {
            while (true)
            {
                var ticks = (DateUtil.EndOfTheDay(DateTime.Now) - DateTime.Now).Ticks;
                _timer.text = new DateTime(ticks).ToString(TimerTemplate);
                
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                
                if (ticks <= 0)
                    SetData();
            }
            // ReSharper disable once FunctionNeverReturns
        }
        
        private void SetData()
        {
            if (_orders.Count != 0)
            {
                _orders.ForEach(x => Destroy(x.gameObject));
                _orders.Clear();
            }
            
            for (var i = 0; i < _orderManager.Count; i++)
            {
                var cell = _orderManagerFactory.CreateItem(_parent);
                _orders.Add(cell);
            }
            
            if (_orderManager.IsNeedRefreshOrders())
            {
                _orderManager.RefreshOrders();
                foreach (var orderCell in _orders)
                {
                    var order = _orderManager.GetRandomOrderByGroup();
                    order.IsActive = true;
                    orderCell.SetData(order);
                }
                
                _orderManager.SendActiveOrders();
            }
            else
            {
                foreach (var orderCell in _orders)
                {
                    var order = _orderManager.GetActiveOrderByGroup(orderCell.Group);
                    orderCell.SetData(order);
                }
            }
        }
        
        private void OnUpgradeClick()
        {
            var canvasT = UiController.GetCanvas(CanvasType.Ui).transform;
            _orderManagerFactory.CreateUpgradePopup(canvasT);
        }
    }
}
