using System;
using System.Collections.Generic;

namespace Components.Scripts.Modules.General.Expedition
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
            Key = data.key;
            Star = data.star;
            TimeEnd = data.timeEnd;

            return this;
        }
        
        public ExpeditionDto ToDto()
        {
            return new ExpeditionDto
            {
                Id = Id,
                key = Key,
                star = Star,
                timeEnd = TimeEnd
            };
        }
    }
}