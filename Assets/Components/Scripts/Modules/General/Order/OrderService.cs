using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Money;
using RoboFactory.General.Profile;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Services;
using RoboFactory.Utils;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace RoboFactory.General.Order
{
    [UsedImplicitly]
    public class OrderService : Service
    {
        protected override string InitializeTextKey => "initialize_2";
        
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;
        [Inject] private readonly MoneyService _moneyService;
        [Inject] private readonly ProductsService productsService;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;

        private readonly Dictionary<string, OrderObject> _ordersDictionary = new();

        public int Count { get; private set; }
        public int Level { get; private set; }
        private long RefreshTime { get; set; }

        private bool _isNeedRefreshForce;
        
        protected override UniTask InitializeAsync()
        {
            if (_ordersDictionary.Count != 0)
                _ordersDictionary.Clear();
            
            foreach (var fileData in _settings.Orders)
            {
                foreach (var data in fileData.Orders)
                {
                    var order = new OrderObject().SetStartData(data);
                    _ordersDictionary.Add(data.Key, order);
                }
            }
            
            var ordersData = _commonProfile.UserProfile.OrdersSection;
            
            Count = ordersData.Count;
            RefreshTime = ordersData.TimeRefresh;
            
            if (ordersData.Orders == null) return UniTask.CompletedTask;
            
            foreach (var orderData in ordersData.Orders)
            {
                var order = _ordersDictionary.First(x => x.Key == orderData.Key);
                order.Value.SetLoadData(orderData.Value);
            }
            
            return UniTask.CompletedTask;
        }

        public void RefreshOrders()
        {
            _ordersDictionary.ToList().ForEach(x =>
            {
                x.Value.IsActive = false;
                x.Value.IsComplete = false;
            });

            RefreshTime = DateUtil.EndOfTheDay(DateTime.Now).ToFileTimeUtc();
            _isNeedRefreshForce = false;
        }
        
        public void RefreshOrdersForce()
        {
            _isNeedRefreshForce = true;
        }
        
        public bool IsNeedRefreshOrders()
        {
            return DateUtil.GetTime(RefreshTime) == 0 || _isNeedRefreshForce;
        }

        public OrderObject GetRandomOrderByGroup()
        {
            var random = new Random().Next(0, _ordersDictionary.Count);
            return _ordersDictionary.ElementAt(random).Value;
        }
        
        public OrderObject GetActiveOrderByGroup(ProductGroup group)
        {
            return _ordersDictionary.Values.First(x => x.Group == group && x.IsActive && !x.IsComplete);
        }

        public async void SendActiveOrders()
        {
            var orders = _ordersDictionary.Where(x => x.Value.IsActive)
                .ToDictionary(x => x.Key, z => z.Value.ToDto());

            await _apiService.SetUserOrders(orders);
        }

        public bool IsEnoughParts(OrderObject orderObject)
        {
            var part = orderObject.Part;
            var itemCount = productsService.GetProduct(part.Data.Key).Count;
            return part.Count <= itemCount;
        }
        
        public void RemoveItems(OrderObject orderObject)
        {
            var item = productsService.GetProduct(orderObject.Part.Data.Key);
            item.DecrementCount(orderObject.Part.Count);
        }
        
        public void CollectMoney(OrderObject orderObject)
        {
            var item = productsService.GetProduct(orderObject.Part.Data.Key);
            _moneyService.PlusMoney(item.Recipe.Cost * orderObject.Part.Count);
        }

        /*private async void IncreaseOrderCount()
        {
            Count++;
            await _apiManager.SetUserOrdersCount(Count);
        }*/
        
        public async UniTask IncreaseOrderLevel()
        {
            Level++;
            await _apiService.SetUserOrdersLevel(Count);
        }

        public UpgradeDataObject GetUpgradeQualityData()
        {
            return null;
        }
        
        [Serializable]
        public class Settings
        {
            [SerializeField] private List<OrderScriptable> _orders;
            
            public List<OrderScriptable> Orders => _orders;
        }
    }
}
