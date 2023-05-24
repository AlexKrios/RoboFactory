using System;

namespace RoboFactory.General.Order
{
    [Serializable]
    public class OrderDto
    {
        public string key;
        public bool isActive;
        public bool isComplete;
    }
}