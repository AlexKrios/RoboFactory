using System;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Item.Products.Models.Object.Types;
using Modules.General.Item.Products.Models.Scriptable;
using Modules.General.Item.Products.Models.Types;

namespace Modules.General.Item.Products.Models
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
