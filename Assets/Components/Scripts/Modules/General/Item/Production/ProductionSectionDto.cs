﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RoboFactory.General.Item.Production
{
    [Serializable]
    public class ProductionSectionDto
    {
        public int level;
        public int count;
        
        [JsonProperty("queue")]
        public Dictionary<string, ProductionDto> Production;
    }
}