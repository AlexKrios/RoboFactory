using System;

namespace RoboFactory.General.Item.Raw
{
    [Serializable]
    public class RawObject : ItemBase
    {
        public RawType RawType { get; set; }

        public int Level { get; set; }

        public RawObject SetInitData(RawScriptable data)
        {
            Key = data.Key;

            ItemType = data.ItemType;
            RawType = data.RawType;

            IconRef = data.IconRef;

            Count = 0;
            Level = 1;

            return this;
        }
        
        public RawDto ToDto()
        {
            return new RawDto
            {
                Count = Count,
                Level = Level
            };
        }
    }
}
