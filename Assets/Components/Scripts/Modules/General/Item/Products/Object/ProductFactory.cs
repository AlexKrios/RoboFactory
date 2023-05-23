﻿using System;
using Components.Scripts.Modules.General.Item.Products.Object.Types;
using Components.Scripts.Modules.General.Item.Products.Scriptable;
using Components.Scripts.Modules.General.Item.Products.Types;

namespace Components.Scripts.Modules.General.Item.Products.Object
{
    public class ProductFactory
    {
        public ProductObject Create(ProductScriptable data)
        {
            switch (data.ProductGroup)
            {
                case ProductGroup.Weapon:
                    return new ProductBuilder(data).Create<WeaponObject>();

                case ProductGroup.Armor:
                    return new ProductBuilder(data).Create<ArmorObject>();
                
                case ProductGroup.Engine:
                    return new ProductBuilder(data).Create<EngineObject>();
                
                case ProductGroup.Battery:
                    return new ProductBuilder(data).Create<BatteryObject>();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}