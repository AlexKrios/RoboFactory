using Modules.General.Order.Models.Scriptable;

namespace Modules.General.Order.Models.Object
{
    public class OrderObjectBuilder
    {
        public OrderObject CreateOrderData(OrderData data)
        {
            return new OrderObject
            {
                key = data.Key,
                group = data.Group,
                part = data.Part
            };
        }
    }
}
