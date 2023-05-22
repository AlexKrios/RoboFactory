using System;
using System.Collections.Generic;
using Components.Scripts.Modules.General.Item.Products.Object.Types;
using Components.Scripts.Modules.General.Item.Raw.Object;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Item
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
