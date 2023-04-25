using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Models;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Scriptable;
using Modules.General.Unit.Type;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Object
{
    [Serializable]
    public class ProductObject : ItemBase
    {
        public int Index { get; set; }
        
        public UnitType UnitType { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public int ProductType { get; set; }
        public bool IsProduct { get; set; }
        
        public int Experience { get; set; }
        public int Level { get; set; }

        public GameObject Model { get; set; }
        
        public List<LevelCap> Caps { get; set; }

        public int GetLevel()
        {
            if (Caps.First().experience > Experience)
                return 1;
            
            if (Caps.Last().experience <= Experience)
                return Caps.Last().level;

            Level = Caps.First(x => x.experience > Experience).level;
            return Level;
        }
        
        public void IncrementExperience() => Experience++;
    }
}