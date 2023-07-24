using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Item.Products
{
    [Serializable]
    public class ProductDto
    {
        [JsonProperty("key")]
        public string Key;
        [JsonProperty("count")]
        public int Count;
        [JsonProperty("experience")]
        public int Experience;
    }
}
