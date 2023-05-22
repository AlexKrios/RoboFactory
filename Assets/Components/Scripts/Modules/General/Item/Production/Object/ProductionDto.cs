using System;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Item.Production.Object
{
    [Serializable]
    public class ProductionDto
    {
        [JsonProperty("id")]
        public Guid Id;
        public string key;
        public int star;
        public long timeEnd;
    }
}
