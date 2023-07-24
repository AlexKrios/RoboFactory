using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Location
{
    [Serializable]
    public class LocationDto
    {
        [JsonProperty("key")]
        public string Key;
        [JsonProperty("level")]
        public int Level;
    }
}