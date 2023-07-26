using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Profile
{
    [Serializable]
    public class CommonSection
    {
        [JsonProperty("name")] 
        public string Name;
    }
}