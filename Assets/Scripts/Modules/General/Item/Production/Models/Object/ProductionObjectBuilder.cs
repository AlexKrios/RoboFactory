using System;
using Modules.General.Item.Models;
using Modules.General.Item.Production.Models.Load;

namespace Modules.General.Item.Production.Models.Object
{
    public class ProductionObjectBuilder
    {
        public ProductionObject Create(ItemBase item, int star)
        {
            var productionTime = item.Recipe.CraftTime;
            return new ProductionObject
            {
                Id = Guid.NewGuid(),
                Key = item.Key,
                Star = star,
                TimeEnd = DateTime.Now.AddSeconds(productionTime).ToFileTime()
            };
        }
        
        public ProductionObject Create(ProductionLoadObject production)
        {
            return new ProductionObject
            {
                Id = production.id,
                Key = production.key,
                Star = production.star,
                TimeEnd = production.timeEnd,
                IsLoad = true
            };
        }
    }
}
