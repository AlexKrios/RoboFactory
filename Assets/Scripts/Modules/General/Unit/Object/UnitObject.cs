using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Scriptable;
using Modules.General.Unit.Type;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Modules.General.Unit.Object
{
    public class UnitObject
    {
        [Inject] private readonly IProductsController _productsController;
        
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

        public UnitObject SetData(UnitScriptable data)
        {
            var products = _productsController.GetUnitDefaultProducts(data.UnitType);
            
            Key = data.Key;
            UnitType = data.UnitType;
            AttackType = data.AttackType;
            IconRef = data.IconRef;
            Model = data.Model;
            Specs = CreateSpecs(data.Specifications);
            Experience = 0;
            Level = 1;
            IsLocked = false;
            Outfit = new List<string>
            {
                products.First(x => x.ProductGroup == ProductGroup.Weapon).Key,
                products.First(x => x.ProductGroup == ProductGroup.Armor).Key,
                products.First(x => x.ProductGroup == ProductGroup.Engine).Key,
                products.First(x => x.ProductGroup == ProductGroup.Battery).Key,
            };

            return this;
        }
        
        private Dictionary<SpecType, int> CreateSpecs(List<int> specsValue)
        {
            var dict = new Dictionary<SpecType, int>();
            for (var i = 0; i < specsValue.Count; i++)
            {
                dict.Add((SpecType) i, specsValue[i]);
            }

            return dict;
        }
        
        [UsedImplicitly]
        public class Factory : PlaceholderFactory<UnitObject> { }
    }
}