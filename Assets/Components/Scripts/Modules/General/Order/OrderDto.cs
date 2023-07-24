using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Order
{
    [Serializable]
    public class OrderDto
    {
        [JsonProperty("key")]
        public string Key;
        [JsonProperty("isActive")]
        public bool IsActive;
        [JsonProperty("isComplete")]
        public bool IsComplete;
    }
}