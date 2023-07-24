using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Unit
{
    [Serializable]
    public class UnitsLoadObject
    {
        [JsonProperty("groupCount")]
        public int GroupCount;
        
        [JsonProperty("units")]
        public Dictionary<string, UnitDto> Units;
    }
}
