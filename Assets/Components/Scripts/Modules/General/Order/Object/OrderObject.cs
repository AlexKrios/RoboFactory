using System;
using Components.Scripts.Modules.General.Item.Models.Recipe;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Order.Scriptable;

namespace Components.Scripts.Modules.General.Order.Object
{
    [Serializable]
    public class OrderObject
    {
        public string Key { get; set; }
        public OrderType Type { get; set; }
        public ProductGroup Group { get; set; }
        public PartObject Part { get; set; }

        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }
        
        public OrderObject SetStartData(OrderData data)
        {
            Key = data.Key;
            Group = data.Group;
            Part = data.Part;

            return this;
        }
        
        public OrderObject SetLoadData(OrderDto dto)
        {
            IsActive = dto.isActive;
            IsComplete = dto.isComplete;

            return this;
        }
        
        public OrderDto ToDto()
        {
            return new OrderDto
            {
                key = Key,
                isActive = IsActive,
                isComplete = IsComplete
            };
        }
    }
}