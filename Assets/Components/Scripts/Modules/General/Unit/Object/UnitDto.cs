using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoboFactory.General.Item.Products;

namespace RoboFactory.General.Unit
{
    [Serializable]
    public class UnitDto
    {
        public string key;
        public int experience;
        public int level;
        
        [JsonProperty("outfit")]
        public Dictionary<ProductGroup, string> Outfit;
        public bool isLocked;
    }
}