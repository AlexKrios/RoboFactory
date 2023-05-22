using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Item.Production.Object
{
    [Serializable]
    public class ProductionSectionDto
    {
        public int level;
        public int count;
        
        [JsonProperty("queue")]
        public Dictionary<string, ProductionDto> Production;
    }
}
