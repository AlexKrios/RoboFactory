using System;
using System.Collections.Generic;

namespace RoboFactory.General.Expedition
{
    public class ExpeditionObject
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Star { get; set; }
        public List<string> Units { get; set; }
        public List<string> Enemy { get; set; }
        
        public long TimeEnd { get; set; }
        
        public ExpeditionObject SetDtoData(ExpeditionDto data)
        {
            Id = data.Id;
            Key = data.Key;
            Star = data.Star;
            TimeEnd = data.TimeEnd;

            return this;
        }
        
        public ExpeditionDto ToDto()
        {
            return new ExpeditionDto
            {
                Id = Id,
                Key = Key,
                Star = Star,
                TimeEnd = TimeEnd
            };
        }
    }
}