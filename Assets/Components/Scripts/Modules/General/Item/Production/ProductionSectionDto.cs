using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Item.Production
{
    [Serializable]
    public class ProductionSectionDto
    {
        [JsonProperty("level")]
        public int Level;
        [JsonProperty("count")]
        public int Count;
        
        [JsonProperty("queue")]
        public Dictionary<string, ProductionDto> Production;
    }
}
