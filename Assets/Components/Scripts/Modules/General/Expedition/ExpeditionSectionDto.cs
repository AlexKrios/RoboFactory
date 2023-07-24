using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Expedition
{
    [Serializable]
    public class ExpeditionSectionDto
    {
        [JsonProperty("count")]
        public int Count;
        
        [JsonProperty("expeditions")]
        public Dictionary<string, ExpeditionDto> Expeditions;
    }
}
