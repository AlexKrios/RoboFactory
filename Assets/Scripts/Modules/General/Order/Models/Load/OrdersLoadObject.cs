using System;
using System.Collections.Generic;

namespace Modules.General.Order.Models.Load
{
    [Serializable]
    public class OrdersLoadObject
    {
        public List<OrderDataLoadObject> orders;
        public int count;
        public long timeRefresh;
    }
    
    [Serializable]
    public class OrderDataLoadObject
    {
        public string key;
        public bool isActive;
        public bool isComplete;
    }
}
