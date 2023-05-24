using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RoboFactory.General.Expedition
{
    [Serializable]
    public class ExpeditionSectionDto
    {
        public int count;
        
        [JsonProperty("expeditions")]
        public Dictionary<string, ExpeditionDto> Expeditions;
    }
}
