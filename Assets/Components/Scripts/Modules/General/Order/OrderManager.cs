using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Money;
using RoboFactory.General.Scriptable;
using RoboFactory.Utils;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace RoboFactory.General.Order
{
    [UsedImplicitly]
    public class OrderManager
    {
        #region Zenject

        [Inject] private readonly ApiService apiService;
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly ProductsManager _productsManager;

        #endregion

        #region Variables
        
        private Dictionary<string, OrderObject> OrdersDictionary { get; }

        public int Count { get; private set; }
        public int Level { get; private set; }
        private long RefreshTime { get; set; }

        private bool _isNeedRefreshForce;

        #endregion

        public OrderManager(Settings settings)
        {
            OrdersDictionary = new Dictionary<string, OrderObject>();
            foreach (var fileData in settings.Orders)
            {
                foreach (var data in fileData.Orders)
                {
                    var order = new OrderObject().SetStartData(data);
                    OrdersDictionary.Add(data.Key, order);
                }
            }
        }

        public void LoadData(OrdersLoadObject data)
        {
            Count = data.Count;
            RefreshTime = data.TimeRefresh;
            
            if (data.Orders == null)
                return;
            
            foreach (var orderData in data.Orders)
            {
                var order = OrdersDictionary.First(x => x.Key == orderData.Key);
                order.Value.SetLoadData(orderData.Value);
            }
        }

        public void RefreshOrders()
        {
            OrdersDictionary.ToList().ForEach(x =>
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
            var random = new Random().Next(0, OrdersDictionary.Count);
            return OrdersDictionary.ElementAt(random).Value;
        }
        
        public OrderObject GetActiveOrderByGroup(ProductGroup group)
        {
            return OrdersDictionary.Values.First(x => x.Group == group && x.IsActive && !x.IsComplete);
        }

        public async void SendActiveOrders()
        {
            var orders = OrdersDictionary.Where(x => x.Value.IsActive)
                .ToDictionary(x => x.Key, z => z.Value.ToDto());

            await apiService.SetUserOrders(orders);
        }

        public bool IsEnoughParts(OrderObject orderObject)
        {
            var part = orderObject.Part;
            var itemCount = _productsManager.GetProduct(part.Data.Key).Count;
            return part.Count <= itemCount;
        }
        
        public void RemoveItems(OrderObject orderObject)
        {
            var item = _productsManager.GetProduct(orderObject.Part.Data.Key);
            item.DecrementCount(orderObject.Part.Count);
        }
        
        public void CollectMoney(OrderObject orderObject)
        {
            var item = _productsManager.GetProduct(orderObject.Part.Data.Key);
            _moneyManager.PlusMoney(item.Recipe.Cost * orderObject.Part.Count);
        }

        /*private async void IncreaseOrderCount()
        {
            Count++;
            await _apiManager.SetUserOrdersCount(Count);
        }*/
        
        public async UniTask IncreaseOrderLevel()
        {
            Level++;
            await apiService.SetUserOrdersLevel(Count);
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
