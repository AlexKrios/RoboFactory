using System.Collections.Generic;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.General.Unit.Models.Object
{
    public class UnitObject
    {
        public string Key { get; set; }

        public UnitType UnitType { get; set; }
        public AttackType AttackType { get; set; }

        public AssetReference IconRef { get; set; }
        public GameObject Model { get; set; }

        public Dictionary<SpecType, int> Specs { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public bool IsLocked { get; set; }
        
        public List<string> Outfit { get; set; }

        public int Attack => Specs[SpecType.Attack];
        public int Health => Specs[SpecType.Health];
        public int Defense => Specs[SpecType.Defense];
        public int Initiative => Specs[SpecType.Initiative];
    }
}