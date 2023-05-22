using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Unit.Object
{
    [Serializable]
    public class UnitsLoadObject
    {
        public int groupCount;
        
        [JsonProperty("units")]
        public Dictionary<string, UnitDto> Units;
    }
}
