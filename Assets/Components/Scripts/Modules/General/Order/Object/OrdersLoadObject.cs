using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Order.Object
{
    [Serializable]
    public class OrdersLoadObject
    {
        public int count;
        public long timeRefresh;
        
        [JsonProperty("orders")]
        public Dictionary<string, OrderDto> Orders;
    }
}
