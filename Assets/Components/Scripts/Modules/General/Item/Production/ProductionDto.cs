using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming
namespace RoboFactory.General.Item.Production
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
