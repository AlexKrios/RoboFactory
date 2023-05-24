using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;

namespace RoboFactory.General.Item
{
    [Serializable]
    public class StoresDto
    {
        [JsonProperty("raw")]
        public Dictionary<string, RawDto> Raw;
        [JsonProperty("products")]
        public Dictionary<string, ProductDto> Products;
    }
}
