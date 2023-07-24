using System;
using Newtonsoft.Json;

namespace RoboFactory.General.Profile
{
    [Serializable]
    public class CommonSection
    {
        [JsonProperty("name")] 
        public string Name;
    }
}