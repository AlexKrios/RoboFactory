using System;
using Components.Scripts.Modules.General.Item.Models;
using Components.Scripts.Modules.General.Item.Raw.Scriptable;

namespace Components.Scripts.Modules.General.Item.Raw.Object
{
    [Serializable]
    public class RawObject : ItemBase
    {
        public RawType RawType { get; set; }

        public int Level { get; set; }
        
        public bool IsRefill { get; set; }

        public RawSettingsObject Settings { get; set; }

        public RawObject SetInitData(RawScriptable data)
        {
            Key = data.Key;

            ItemType = data.ItemType;
            RawType = data.RawType;

            IconRef = data.IconRef;

            Count = 0;
            Level = 1;

            IsRefill = data.IsMain;

            return this;
        }
        
        public RawDto ToDto()
        {
            return new RawDto
            {
                count = Count,
                level = Level
            };
        }
    }
}
