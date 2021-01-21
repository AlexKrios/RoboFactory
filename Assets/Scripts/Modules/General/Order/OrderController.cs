using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Money;
using Modules.General.Order.Models.Load;
using Modules.General.Order.Models.Object;
using Modules.General.Order.Models.Scriptable;
using Utils;
using Zenject;

namespace Modules.General.Order
{
    [UsedImplicitly]
    public class OrderController : IOrderController
    {
        #region Zenject

        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly IProductsController _productsController;

        #endregion

        #region Variables
        
        public List<OrderObject> OrdersList { get; }

        public int OrderCount { get; private set; }
        public long RefreshTime { get; private set; }

        private bool _isNeedRefreshForce;

        #endregion

        public OrderController(Settings settings)
        {
            OrdersList = new List<OrderObject>();

            foreach (var fileData in settings.orders)
            {
                foreach (var data in fileData.Orders)
                {
                    var order = new OrderObjectBuilder().CreateOrderData(data);
                    
                    OrdersList.Add(order);
                }
            }
        }

        public void LoadOrders(OrdersLoadObject ordersInfo)
        {
            OrderCount = ordersInfo.count;
            RefreshTime = ordersInfo.timeRefresh;
            foreach (var orderData in ordersInfo.orders)
            {
                var order = OrdersList.First(x => x.key == orderData.key);
                order.isActive = orderData.isActive;
                order.isComplete = orderData.isComplete;
            }
        }

        public void RefreshOrders()
        {
            OrdersList.ForEach(x =>
            {
                x.isActive = false;
                x.isComplete = false;
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

        public OrderObject GetRandomOrderByGroup(ProductGroup group)
        {
            var orders = OrdersList.Where(x => x.group == group).ToList();
            var random = new Random().Next(0, orders.Count);
            return orders[random];
        }
        
        public OrderObject GetActiveOrderByGroup(ProductGroup group)
        {
            return OrdersList.First(x => x.group == group && x.isActive && !x.isComplete);
        }

        public bool IsEnoughParts(OrderObject orderObject)
        {
            var part = orderObject.part;
            var itemCount = _productsController.GetProduct(part.data.Key).Count;
            return part.count <= itemCount;
        }
        
        public void RemoveItems(OrderObject orderObject)
        {
            var item = _productsController.GetProduct(orderObject.part.data.Key);
            item.DecrementCount(orderObject.part.count);
        }
        
        public void CollectMoney(OrderObject orderObject)
        {
            var item = _productsController.GetProduct(orderObject.part.data.Key);
            _moneyController.PlusMoney(item.Recipe.Cost * orderObject.part.count);
        }
        
        [Serializable]
        public class Settings
        {
            public List<OrderScriptable> orders;
        }
    }
}
