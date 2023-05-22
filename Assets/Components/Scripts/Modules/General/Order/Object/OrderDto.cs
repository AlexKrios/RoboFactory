using System;

namespace Components.Scripts.Modules.General.Order.Object
{
    [Serializable]
    public class OrderDto
    {
        public string key;
        public bool isActive;
        public bool isComplete;
    }
}