using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RoboFactory.General.Unit
{
    [Serializable]
    public class UnitsLoadObject
    {
        public int groupCount;
        
        [JsonProperty("units")]
        public Dictionary<string, UnitDto> Units;
    }
}
