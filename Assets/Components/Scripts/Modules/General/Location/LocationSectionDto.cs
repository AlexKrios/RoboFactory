using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RoboFactory.General.Location
{
    [Serializable]
    public class LocationSectionDto
    {
        [JsonProperty("locations")]
        public Dictionary<string, LocationDto> Locations;
    }
}
