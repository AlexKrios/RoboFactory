using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoboFactory.General.Item.Products;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Unit
{
    [Serializable]
    public class UnitDto
    {
        [JsonProperty("key")] 
        public string Key;
        [JsonProperty("experience")] 
        public int Experience;
        [JsonProperty("level")] 
        public int Level;
        
        [JsonProperty("outfit")]
        public Dictionary<ProductGroup, string> Outfit;
        public bool IsLocked { get; set; }
    }
}