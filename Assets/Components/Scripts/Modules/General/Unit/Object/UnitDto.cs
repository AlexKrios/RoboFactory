using System;
using System.Collections.Generic;
using Components.Scripts.Modules.General.Item.Products.Types;
using Newtonsoft.Json;

namespace Components.Scripts.Modules.General.Unit.Object
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