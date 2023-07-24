using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Item.Raw
{
    [Serializable]
    public class RawDto
    {
        [JsonProperty("count")]
        public int Count;
        [JsonProperty("level")]
        public int Level;
    }
}
