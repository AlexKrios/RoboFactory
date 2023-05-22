using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Location
{
    [Serializable]
    public class LocationSectionDto
    {
        [JsonProperty("locations")]
        public Dictionary<string, LocationDto> Locations;
    }
}
