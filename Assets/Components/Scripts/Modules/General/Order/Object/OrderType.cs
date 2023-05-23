using System;

namespace Components.Scripts.Modules.General.Order.Object
{
    [Serializable]
    public enum OrderType
    {
        None = -1,
        Production = 1,
        Sale = 2
    }
}