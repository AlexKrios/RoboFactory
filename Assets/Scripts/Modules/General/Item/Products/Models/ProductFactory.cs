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
                case ProductGroup.Skill:
                    return new ProductBuilder(data).Create<EquipmentObject>();
                
                case ProductGroup.Weapon:
                    return new ProductBuilder(data).Create<WeaponObject>();

                case ProductGroup.Armor:
                    return new ProductBuilder(data).Create<ArmorObject>();
                
                case ProductGroup.Equipment:
                    return new ProductBuilder(data).Create<SkillObject>();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
