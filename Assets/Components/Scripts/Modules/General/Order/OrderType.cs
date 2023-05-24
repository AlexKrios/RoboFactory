using System;

namespace RoboFactory.General.Order
{
    [Serializable]
    public enum OrderType
    {
        None = -1,
        Production = 1,
        Sale = 2
    }
}