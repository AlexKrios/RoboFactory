﻿using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Item.Products;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Unit.Scriptable;
using Components.Scripts.Modules.General.Unit.Type;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Components.Scripts.Modules.General.Unit.Object
{
    [UsedImplicitly]
    public class UnitObject
    {
        [Inject] private readonly ProductsManager _productsManager;

        public string Key { get; set; }

        public UnitType UnitType { get; set; }
        public AttackType AttackType { get; set; }

        public AssetReference IconRef { get; set; }
        public GameObject Model { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public bool IsLocked { get; set; }

        //public List<string> Outfit { get; set; }
        
        public Dictionary<ProductGroup, string> Outfit { get; set; }
        public Dictionary<SpecType, int> Specs { get; set; }

        public int Attack => Specs[SpecType.Attack];
        public int Health => Specs[SpecType.Health];
        public int Defense => Specs[SpecType.Defense];
        public int Initiative => Specs[SpecType.Initiative];

        public UnitObject SetData(UnitScriptable data)
        {
            var products = _productsManager.GetUnitDefaultProducts(data.UnitType);
            
            Key = data.Key;
            UnitType = data.UnitType;
            AttackType = data.AttackType;
            IconRef = data.IconRef;
            Model = data.Model;
            Specs = CreateSpecs(data.Specifications);
            Experience = 0;
            Level = 1;
            IsLocked = false;
            /*Outfit = new List<string>
            {
                products.First(x => x.ProductGroup == ProductGroup.Weapon).Key,
                products.First(x => x.ProductGroup == ProductGroup.Armor).Key,
                products.First(x => x.ProductGroup == ProductGroup.Engine).Key,
                products.First(x => x.ProductGroup == ProductGroup.Battery).Key,
            };*/

            Outfit = new Dictionary<ProductGroup, string>
            {
                [ProductGroup.Weapon] = products.First(x => x.ProductGroup == ProductGroup.Weapon).Key,
                [ProductGroup.Armor] = products.First(x => x.ProductGroup == ProductGroup.Armor).Key,
                [ProductGroup.Engine] = products.First(x => x.ProductGroup == ProductGroup.Engine).Key,
                [ProductGroup.Battery] = products.First(x => x.ProductGroup == ProductGroup.Battery).Key, 
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

        public UnitDto ToDto()
        {
            return new UnitDto
            {
                key = Key,
                experience = Experience,
                level = Level,
                Outfit = Outfit,
                isLocked = IsLocked
            };
        }
        
        [UsedImplicitly]
        public class Factory : PlaceholderFactory<UnitObject> { }
    }
}