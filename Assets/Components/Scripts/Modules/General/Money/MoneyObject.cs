using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Money
{
    [Serializable]
    public class MoneyObject
    {
        [JsonProperty("money")]
        public int Money;
    }
}
