using System.Collections.Generic;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Order.Models.Load;
using Modules.General.Order.Models.Object;

namespace Modules.General.Order
{
    public interface IOrderController
    {
        List<OrderObject> OrdersList { get; }

        int OrderCount { get; }
        long RefreshTime { get; }

        void LoadOrders(OrdersLoadObject ordersLoadObject);
        bool IsNeedRefreshOrders();
        void RefreshOrders();
        void RefreshOrdersForce();
        OrderObject GetRandomOrderByGroup(ProductGroup @group);
        OrderObject GetActiveOrderByGroup(ProductGroup @group);

        bool IsEnoughParts(OrderObject orderObject);
        void RemoveItems(OrderObject orderObject);
        void CollectMoney(OrderObject orderObject);
    }
}
