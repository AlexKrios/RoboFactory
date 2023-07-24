using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Level
{
    [Serializable]
    public class LevelObject
    {
        [JsonProperty("level")]
        public int Level;
        [JsonProperty("experience")]
        public int Experience;
    }
}
