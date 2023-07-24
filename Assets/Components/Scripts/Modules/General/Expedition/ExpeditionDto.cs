using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Expedition
{
    [Serializable]
    public class ExpeditionDto
    {
        [JsonProperty("id")]
        public Guid Id;
        [JsonProperty("key")]
        public string Key;
        [JsonProperty("star")]
        public string Star;
        [JsonProperty("timeEnd")]
        public long TimeEnd;
    }
}