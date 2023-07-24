using System;
using RoboFactory.General.Item;
using RoboFactory.General.Item.Products;

namespace RoboFactory.General.Order
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
            IsActive = dto.IsActive;
            IsComplete = dto.IsComplete;

            return this;
        }
        
        public OrderDto ToDto()
        {
            return new OrderDto
            {
                Key = Key,
                IsActive = IsActive,
                IsComplete = IsComplete
            };
        }
    }
}