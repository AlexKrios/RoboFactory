﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RoboFactory.General.Order
{
    [Serializable]
    public class OrdersLoadObject
    {
        public int count;
        public int level;
        public long timeRefresh;
        
        [JsonProperty("orders")]
        public Dictionary<string, OrderDto> Orders;
    }
}