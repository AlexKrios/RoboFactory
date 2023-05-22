using System;
using Components.Scripts.Modules.General.Item.Models;

namespace Components.Scripts.Modules.General.Item.Production.Object
{
    [Serializable]
    public class ProductionObject
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public int Star { get; set; }

        public long TimeEnd { get; set; }
        public bool IsLoad { get; set; }

        public ProductionObject SetInitData(ItemBase item, int star)
        {
            var productionTime = item.Recipe.CraftTime;
            Id = Guid.NewGuid();
            Key = item.Key;
            Star = star;
            TimeEnd = DateTime.Now.AddSeconds(productionTime).ToFileTime();

            return this;
        }
        
        public ProductionObject SetLoadData(ProductionDto dto)
        {
            Id = dto.Id;
            Key = dto.key;
            Star = dto.star;
            TimeEnd = dto.timeEnd;
            IsLoad = true;

            return this;
        }
        
        public ProductionDto ToDto()
        {
            return new ProductionDto
            {
                Id = Id,
                key = Key,
                star = Star,
                timeEnd = TimeEnd
            };
        }
    }
}