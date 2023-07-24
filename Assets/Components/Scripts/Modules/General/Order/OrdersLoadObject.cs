using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Order
{
    [Serializable]
    public class OrdersLoadObject
    {
        [JsonProperty("count")]
        public int Count;
        [JsonProperty("level")]
        public int Level;
        [JsonProperty("timeRefresh")]
        public long TimeRefresh;
        
        [JsonProperty("orders")]
        public Dictionary<string, OrderDto> Orders;
    }
}
